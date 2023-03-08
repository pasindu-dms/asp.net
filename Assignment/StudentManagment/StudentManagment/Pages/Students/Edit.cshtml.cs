using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StudetManagement.Pages.Students
{
    public class EditModel : PageModel
    {
        public StudentInfo studentinfo=new StudentInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=DESKTOP-3BL537V\\SQLEXPRESS01;Initial Catalog=students;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM students WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                studentinfo.id = "" + reader.GetInt32(0);
                                studentinfo.name= reader.GetString(1);
                                studentinfo.reg_id=reader.GetString(2);
                                studentinfo.indexNum = reader.GetString(3);
                                studentinfo.phone=reader.GetString(4);
                                studentinfo.address=reader.GetString(5);

                               
                            }

                        }

                    }


                }



            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            studentinfo.id = Request.Form["id"];
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

            try
            {
                String connectionString = "Data Source=DESKTOP-3BL537V\\SQLEXPRESS01;Initial Catalog=students;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE students " +
                        "SET name=@name,registration_num=@reg_num, index_num=@index_num , phone=@phone, address=@address "+
                        "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", studentinfo.name);
                        command.Parameters.AddWithValue("@reg_num", studentinfo.reg_id);
                        command.Parameters.AddWithValue("@index_num", studentinfo.indexNum);
                        command.Parameters.AddWithValue("@phone", studentinfo.phone);
                        command.Parameters.AddWithValue("@address", studentinfo.address);
                        command.Parameters.AddWithValue("@id", studentinfo.id);

                        command.ExecuteNonQuery();

                    }

                }


            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Students/Index");

        }
    }
}
