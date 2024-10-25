using firefishBackEnd.Models;
using System.Data;
using System.Data.SqlClient;

namespace firefishBackEnd.Services
{
    public class CandidateService
    {
        SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Database=Web_API_Task;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

        public List<CandidateListingItem> GetCandidates()
        {
            List<CandidateListingItem> candidates = new List<CandidateListingItem>();

            conn.Open();

            using (SqlCommand command = new SqlCommand(@"SELECT 
                   DISTINCT ID,FirstName,Surname,DateOfBirth,
                    ISNULL(stuff((
                    SELECT ',' + s2.name
                    FROM Skill s2
		            JOIN CandidateSkill cs ON cs.SkillID = s2.ID
                    WHERE cs.CandidateID = c.ID
                    ORDER BY s2.Name
                    FOR xml path('')
                ),1,1,''),'') as Skills, 
            	Address1, Town, Country, PostCode, PhoneHome, PhoneMobile, PhoneWork, UpdatedDate
                FROM Candidate c
                GROUP BY ID,FirstName,Surname,DateOfBirth, Address1, Town, Country, PostCode, PhoneHome, PhoneMobile, PhoneWork, UpdatedDate
                ORDER BY UpdatedDate DESC", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    candidates.Add(new CandidateListingItem() { 
                        ID = reader.GetInt32(0),
                        FirstName = reader.GetString(1), 
                        Surname = reader.GetString(2), 
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]).ToString("dd/MM/yyyy"),
                        SkillNamesConcatanated = reader.GetString(4),
                        Address1 = SafeGetString(reader, 5),
                        Town = SafeGetString(reader, 6),
                        Country = SafeGetString(reader, 7),
                        PostCode = SafeGetString(reader, 8),
                        PhoneHome = SafeGetString(reader, 9),
                        PhoneMobile = SafeGetString(reader, 10),
                        PhoneWork = SafeGetString(reader, 11)
                    });
                }
            }

            conn.Close();
            return candidates;
        }

        public Candidate GetCandidate(int ID)
        {
            Candidate candidate = new();

            conn.Open();
            String SQL_GetCandidate = @"SELECT 
                ID,FirstName,Surname,DateOfBirth, Address1, Town, Country, PostCode, PhoneHome, PhoneMobile, PhoneWork, UpdatedDate
                FROM Candidate c
                WHERE ID = @candidateID";
            using (SqlCommand command = new SqlCommand(SQL_GetCandidate, conn))
            {
                command.Parameters.Add("@candidateID", SqlDbType.Int).Value = ID;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    candidate = new Candidate()
                    {
                        ID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        Surname = reader.GetString(2),
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]).ToString("dd/MM/yyyy"),
                        Address1 = SafeGetString(reader, 4),
                        Town = SafeGetString(reader, 5),
                        Country = SafeGetString(reader, 6),
                        PostCode = SafeGetString(reader, 7),
                        PhoneHome = SafeGetString(reader, 8),
                        PhoneMobile = SafeGetString(reader, 9),
                        PhoneWork = SafeGetString(reader, 10)
                    };
                }
            }
            conn.Close();
            conn.Open();

            candidate.SkillIDs = new List<int>();

            using (SqlCommand command = new SqlCommand(@"SELECT SkillID FROM CandidateSkill
                WHERE CandidateID = @candidateID", conn))
            {
                command.Parameters.Add("@candidateID", SqlDbType.Int).Value = ID;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    candidate.SkillIDs.Add(reader.GetInt32(0));
                }
            }

            conn.Close();
            return candidate;
        }

        public bool CreateCandidate(Candidate candidate)
        {
            conn.Open();

            String SQL =
            @"INSERT INTO Candidate (ID,FirstName,Surname,DateOfBirth,Address1,Town,Country,PostCode,PhoneHome,PhoneMobile,PhoneWork,CreatedDate, UpdatedDate)" +
            "VALUES ((SELECT MAX(ID)+1 FROM Candidate),@FirstName,@Surname,@DateOfBirth,@Address1,@Town,@Country,@PostCode,@PhoneHome,@PhoneMobile,@PhoneWork, GETDATE(), GETDATE())";
            using (SqlCommand cmd = new SqlCommand(SQL, conn))
            {
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = candidate.FirstName;
                cmd.Parameters.Add("@Surname", SqlDbType.VarChar, 50).Value = candidate.Surname;
                cmd.Parameters.Add(new SqlParameter("@Address1", candidate.Address1 == null? DBNull.Value: candidate.Address1));
                cmd.Parameters.Add(new SqlParameter("@DateOfBirth", candidate.DateOfBirth == null ? DBNull.Value : candidate.DateOfBirth));
                cmd.Parameters.Add(new SqlParameter("@Town", candidate.Town == null ? DBNull.Value : candidate.Town));
                cmd.Parameters.Add(new SqlParameter("@Country", candidate.Country == null ? DBNull.Value : candidate.Country));
                cmd.Parameters.Add(new SqlParameter("@PostCode", candidate.PostCode == null ? DBNull.Value : candidate.PostCode));
                cmd.Parameters.Add(new SqlParameter("@PhoneHome", candidate.PhoneHome == null ? DBNull.Value : candidate.PhoneHome));
                cmd.Parameters.Add(new SqlParameter("@PhoneMobile", candidate.PhoneMobile == null ? DBNull.Value : candidate.PhoneMobile));
                cmd.Parameters.Add(new SqlParameter("@PhoneWork", candidate.PhoneWork == null ? DBNull.Value : candidate.PhoneWork));
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }

            conn.Close();
            return true;
        }
        public bool UpdateCandidate(Candidate candidate)
        {
            conn.Open();

            String SQL =
            @"UPDATE Candidate SET FirstName=@FirstName,Surname=@Surname,DateOfBirth=@DateOfBirth,Address1=@Address1,Town=@Town,Country=@Country,PostCode=@PostCode,PhoneHome=@PhoneHome,PhoneMobile=@PhoneMobile,PhoneWork=PhoneWork, UpdatedDate=GETDATE() WHERE ID=@ID";
            using (SqlCommand cmd = new SqlCommand(SQL, conn))
            {
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = candidate.FirstName;
                cmd.Parameters.Add("@Surname", SqlDbType.VarChar, 50).Value = candidate.Surname;
                cmd.Parameters.Add(new SqlParameter("@Address1", candidate.Address1 == null ? DBNull.Value : candidate.Address1));
                cmd.Parameters.Add(new SqlParameter("@DateOfBirth", candidate.DateOfBirth == null ? DBNull.Value : candidate.DateOfBirth));
                cmd.Parameters.Add(new SqlParameter("@Town", candidate.Town == null ? DBNull.Value : candidate.Town));
                cmd.Parameters.Add(new SqlParameter("@Country", candidate.Country == null ? DBNull.Value : candidate.Country));
                cmd.Parameters.Add(new SqlParameter("@PostCode", candidate.PostCode == null ? DBNull.Value : candidate.PostCode));
                cmd.Parameters.Add(new SqlParameter("@PhoneHome", candidate.PhoneHome == null ? DBNull.Value : candidate.PhoneHome));
                cmd.Parameters.Add(new SqlParameter("@PhoneMobile", candidate.PhoneMobile == null ? DBNull.Value : candidate.PhoneMobile));
                cmd.Parameters.Add(new SqlParameter("@PhoneWork", candidate.PhoneWork == null ? DBNull.Value : candidate.PhoneWork));
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = candidate.ID;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
            return true;
        }
        public static string SafeGetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
    }

}