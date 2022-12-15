using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webappAdoPractice
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            showData();

            SqlConnection con = new SqlConnection("data source = DESKTOP-HDRIDJ6\\SQLEXPRESS; database = dropdown; integrated security = SSPI ");
            SqlCommand comand = new SqlCommand("select * from city ", con);
            con.Open();

            //SqlDataReader sqr = comand.ExecuteReader();
            SqlDataAdapter sda = new SqlDataAdapter(comand);
           
            DataTable dt = new DataTable();
            sda.Fill(dt);

            ddlCity.DataSource = dt;

            ddlCity.DataTextField = "city_name";

            ddlCity.DataValueField = "id";

            ddlCity.DataBind();

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("data source = DESKTOP-HDRIDJ6\\SQLEXPRESS; database = dropdown; integrated security = SSPI ");

            string name = txtName.Text;
            int age = Convert.ToInt32 (txtAge.Text) ;
            int cityID =Convert.ToInt32(ddlCity.SelectedValue);



            string folder = Server.MapPath("/images/");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            imagePath.SaveAs(folder + Path.GetFileName(imagePath.FileName));

            string path = folder + Path.GetFileName(imagePath.FileName);
            
           


            //Response.Write(name + age + cityID);

            string query = $"insert into customers(city_id , name ,age ,image) values ('{cityID}','{name}',{age},'{path}') ";
            SqlCommand comand = new SqlCommand(query, con);
            con.Open();
            comand.ExecuteNonQuery();


            showData();
            

        }

        public void showData() {

            SqlConnection con = new SqlConnection("data source = DESKTOP-HDRIDJ6\\SQLEXPRESS; database = dropdown; integrated security = SSPI ");
            SqlCommand comand = new SqlCommand("select * from customers ", con);
            con.Open();

            SqlDataReader sqr = comand.ExecuteReader();

            string table = "<table class = 'table table-striped'><tr><th>id</th>  <th>city id</th>  <th>Name</th>  <th>Age</th> <th>image</th>  </tr> ";



            while (sqr.Read()) {
                string path = sqr[4].ToString() ;
                int i = path.IndexOf("images");
                string p = path.Substring(i) ;
               // path.Substring(40, path.Length - 1);
                table += "<tr>" +
                     $"<td>{sqr[0]}</td>" +
                     $"<td>{sqr[1]}</td>" +
                     $"<td>{sqr[2]}</td>" +
                     $"<td>{sqr[3]}</td>" +
                     $"<td><img src={p} ></td>" +
                     "</tr>";

            }
            table += "</table>";
            Label l = new Label();
            l.Text = table;
            this.Controls.Add(l);
        }
    }
}