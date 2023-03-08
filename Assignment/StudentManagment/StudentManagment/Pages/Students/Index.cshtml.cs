using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudetManagement.Pages.Students
{
    public class IndexModel : PageModel
    {
        public List<StudentInfo> ListStudents =new List<StudentInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-3BL537V\\SQLEXPRESS01;Initial Catalog=students;Integrated Security=True";

                using( SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "SELECT * FROM students";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            while (reader.Read()) 
                            {
                                StudentInfo studentInfo= new StudentInfo();
                                studentInfo.id=""+reader.GetInt32(0);
                                studentInfo.name=reader.GetString(1);
                                studentInfo.reg_id = reader.GetString(2);
                                studentInfo.indexNum = reader.GetString(3);
                                studentInfo.phone=reader.GetString(4);
                                studentInfo.address=reader.GetString(5);
                                studentInfo.createdAt = reader.GetDateTime(6).ToString();

                                ListStudents.Add(studentInfo);




                            }
                        
                        }
                    
                    }
                
                
                }


            }
            catch(Exception ex)

            {
                Console.WriteLine("Exception: "+ex.Message);

            }
        }
    }

    public class StudentInfo
    {
        public String id;
        public String name;
        public String reg_id;
        public String indexNum;
        public String phone;
        public String address;
        public String createdAt;


    }
}
