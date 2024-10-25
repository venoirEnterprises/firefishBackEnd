using firefishBackEnd.Models;
using System.Data.SqlClient;

namespace firefishBackEnd.Services
{
    public class SkillsService
    {
        SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Database=Web_API_Task;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

        public List<Skill> GetSkills()
        {
            List<Skill> skills = new List<Skill>();

            conn.Open();

            using (SqlCommand command = new SqlCommand(@"SELECT ID,Name FROM Skill ORDER BY Name ASC", conn))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    skills.Add(new Skill()
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
            }

            conn.Close();
            return skills;
        }
    }
}
