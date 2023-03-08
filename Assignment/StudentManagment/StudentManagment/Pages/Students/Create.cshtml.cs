using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudetManagement.Pages.Students
{
    public class CreateModel : PageModel
    {
        public StudentInfo studentinfo = new StudentInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            studentinfo.name = Request.Form["name"];
            studentinfo.reg_id = Request.Form["reg_num"];
            studentinfo.indexNum = Request.Form["indexNum"];
            studentinfo.phone = Request.Form["mobile"];
            studentinfo.address = Request.Form["address"];

            if (studentinfo.name.Length == 0 || studentinfo.reg_id.Length == 0 || studentinfo.indexNum.Length == 0 ||
                studentinfo.phone.Length == 0 || studentinfo.address.Length == 0)
            {
                errorMessage = "All the Fields are Required";
                return;
            }

            //save the students
            try
            {
                String connectionString = "Data Source=DESKTOP-3BL537V\\SQLEXPRESS01;Initial Catalog=students;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO  students" +
                        "(name,registration_num,index_num,phone,address)  VALUES" +
                        "(@name,@reg_num,@index_num,@phone,@address);";
                    using (SqlCommand command=new SqlCommand(sql,connection))
                    {
                        command.Parameters.AddWithValue("@name", studentinfo.name);
                        command.Parameters.AddWithValue("@reg_num", studentinfo.reg_id);
                        command.Parameters.AddWithValue("@index_num", studentinfo.indexNum);
                        command.Parameters.AddWithValue("@phone", studentinfo.phone);
                        command.Parameters.AddWithValue("@address", studentinfo.address);

                        command.ExecuteNonQuery();

                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;

            }



            studentinfo.name = "";
            studentinfo.reg_id = "";
            studentinfo.indexNum = "";
            studentinfo.phone = "";
            studentinfo.address ="";

            successMessage = "New student Added Correctly";

            Response.Redirect("/Students/Index");


        }
    }
}
