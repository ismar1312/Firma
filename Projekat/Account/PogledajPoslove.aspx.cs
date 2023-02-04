using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity.Core.Mapping;

namespace Projekat.Account
{
    public partial class PogledajPoslove : System.Web.UI.Page
    {
        SqlConnection connection;
        protected void Page_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(GetConnectionString());

            UcitajGrid(connection);
        }

        protected void UcitajGrid(SqlConnection connection)
        {
            try
            {
                connection.Open();

                string upit = "SELECT * FROM ListaPoslova";
                SqlDataAdapter adapter = new SqlDataAdapter(upit,connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                GridView1.DataSource = table;
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Label3.Text = "Greska";
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                connection.Close();
            }
        }

        string GetConnectionString()
        {
            return "Data Source=DESKTOP-CDPPIKN\\SQLEXPRESS;Initial Catalog=Firma;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "" || TextBox2.Text == "")
            {
                Label3.Text = "Polja moraju biti popunjena.";
                return;
            }

            try
            {
                connection.Open();

                string opisPosla = TextBox1.Text;
                string rok = TextBox2.Text;

                UnesiPosao(connection, opisPosla, rok);

                Response.Redirect("~/Account/PogledajPoslove.aspx", false);
            }
            catch (Exception ex)
            {
                Label3.Text = "Greska";
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                connection.Close();
                UcitajGrid(connection);
            }
        }

        protected void UnesiPosao(SqlConnection connection, string opisPosla, string rok)
        {
            SqlParameter p1 = new SqlParameter();
            SqlParameter p2 = new SqlParameter();

            p1.Value = opisPosla;
            p2.Value = rok;

            p1.ParameterName = "opisPosla";
            p2.ParameterName = "rok";

            string upit = "INSERT INTO ListaPoslova (opisPosla, rok) VALUES (@opisPosla, @rok)";
            SqlCommand command = new SqlCommand(upit, connection);
            command.Parameters.Add(p1);
            command.Parameters.Add(p2);
            command.ExecuteNonQuery();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;

            if(row == null)
            {
                Label3.Text = "Izaberite red!";
                return;
            }

            TextBox1.Text = row.Cells[2].Text;
            TextBox2.Text = row.Cells[3].Text;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if(GridView1.SelectedRow == null)
            {
                Label3.Text = "Izaberite red.";
                return;
            }

            try
            {
                connection.Open();

                int posaoID = int.Parse(GridView1.SelectedRow.Cells[1].Text);

                ObrisiPosao(connection, posaoID);

                Response.Redirect("~/Account/PogledajPoslove.aspx", false);
            }
            catch (Exception ex)
            {
                Label3.Text = "Greska";
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                connection.Close();
            }
        }

        protected void ObrisiPosao(SqlConnection connection, int posaoID)
        {
            SqlParameter p1 = new SqlParameter();
            
            p1.Value = posaoID;
            p1.ParameterName = "posaoID";

            string upit = "DELETE FROM ListaPoslova WHERE posaoID = @posaoID";
            SqlCommand command = new SqlCommand(upit, connection);
            command.Parameters.Add(p1);
            command.ExecuteNonQuery();
        }
    }
}