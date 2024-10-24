using firefishBackEnd.Models;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace firefishBackEnd.Services
{
    public class CandidateService
    {

        SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Database=Web_API_Task;Integrated Security=True;Connect Timeout=30;Encrypt=False;");


        public List<Candidate> GetCandidates()
        {
            List<Candidate> candidates = new List<Candidate>();

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
                ),1,1,''),'') as Skills
                from Candidate c
                GROUP BY ID,FirstName,Surname,DateOfBirth
                ORDER BY ID ASC", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    candidates.Add(new Candidate() { 
                        firstName = reader.GetString(1), 
                        surname = reader.GetString(2), 
                        dateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]).ToString("dd/MM/yyyy"),
                        skills = reader.GetString(4)
                    });
                }
            }

            conn.Close();
            return candidates;
        }
    }

}