using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;

namespace Projekat.Admin
{
    public partial class OrganizujPodatke : System.Web.UI.Page
    {
        SqlConnection connection;
        protected void Page_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(GetConnectionString());

            UcitajGrid(connection);
            UcitajGrid2(connection);
            UcitajDropDown(connection);
        }

        protected void UcitajGrid(SqlConnection connection)
        {
            try
            {
                connection.Open();

                string upit = "SELECT * FROM Radnici";
                SqlDataAdapter adapter = new SqlDataAdapter(upit, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                GridView1.DataSource = table;
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "Greska";
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                connection.Close();
            }
        }

        protected void UcitajGrid2(SqlConnection connection)
        {
            try
            {
                connection.Open();

                string upit = "SELECT * FROM RadnoMesto";
                SqlDataAdapter adapter = new SqlDataAdapter(upit, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                GridView2.DataSource = table;
                GridView2.DataBind();

            }
            catch (Exception ex)
            {
                ErrorLabel2.Text = "Greska";
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                connection.Close();
            }
        }

        protected void UcitajDropDown(SqlConnection connection)
        {
            if(!IsPostBack)
            {
                try
                {
                    connection.Open();

                    string upit = "SELECT nazivMesta FROM RadnoMesto";
                    SqlCommand command = new SqlCommand(upit, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        DropDownList1.Items.Add(reader[0].ToString());
                    }

                    reader.Close();

                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = "Greska";
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        string GetConnectionString()
        {
            return "Data Source=DESKTOP-CDPPIKN\\SQLEXPRESS;Initial Catalog=Firma;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // unos novog radnika
            if (TextBox1.Text == "" || TextBox3.Text == "")
            {
                Label4.Text = "Polja moraju biti popunjena.";
                return;
            }

            try
            {
                connection.Open();

                string imePrezime = TextBox1.Text;
                string radnoMesto = DropDownList1.SelectedItem.Value;
                int plata = int.Parse(TextBox3.Text);

                UnesiRadnika(connection, imePrezime, radnoMesto, plata);

                Response.Redirect("~/Admin/OrganizujPodatke.aspx", false);
            }
            catch (Exception ex)
            {
                Label4.Text = "Greska";
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                connection.Close();
            }
        }

        protected void UnesiRadnika(SqlConnection connection, string imePrezime, string radnoMesto, int plata)
        {
            SqlParameter p1 = new SqlParameter();
            SqlParameter p2 = new SqlParameter();
            SqlParameter p3 = new SqlParameter();

            p1.Value = imePrezime;
            p2.Value = radnoMesto;
            p3.Value = plata;

            p1.ParameterName = "imePrezime";
            p2.ParameterName = "radnoMesto";
            p3.ParameterName = "plata";

            string upit = "INSERT INTO Radnici (imePrezime, radnoMesto, plata) VALUES (@imePrezime, @radnoMesto, @plata)";
            SqlCommand command = new SqlCommand(upit, connection);
            command.Parameters.Add(p1);
            command.Parameters.Add(p2);
            command.Parameters.Add(p3);
            command.ExecuteNonQuery();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;

            if (row == null)
            {
                Label3.Text = "Izaberite red!";
                return;
            }

            TextBox1.Text = row.Cells[2].Text;
            try
            {
                DropDownList1.SelectedValue = row.Cells[3].Text;
            }
            catch (Exception ex)
            {
                Label4.Text = "Greska";
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            TextBox3.Text = row.Cells[4].Text;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (GridView1.SelectedRow == null)
            {
                Label4.Text = "Radnik kog zelite da obristete mora biti izabran.";
                return;
            }

            try
            {
                connection.Open();

                int radnikID = int.Parse(GridView1.SelectedRow.Cells[1].Text);

                ObrisiRadnika(connection, radnikID);

                Response.Redirect("~/Admin/OrganizujPodatke.aspx", false);
            }
            catch (Exception ex)
            {
                Label4.Text = "Greska";
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                connection.Close();
            }
        }

        protected void ObrisiRadnika(SqlConnection connection, int radnikID)
        {
            SqlParameter p1 = new SqlParameter();

            p1.Value = radnikID;
            p1.ParameterName = "radnikID";

            string upit = "DELETE FROM Radnici WHERE radnikID = @radnikID";
            SqlCommand command = new SqlCommand(upit, connection);
            command.Parameters.Add(p1);
            command.ExecuteNonQuery();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "" || TextBox3.Text == "" || GridView1.SelectedRow == null)
            {
                Label4.Text = "Polja moraju biti popunjena a radnik kog zelite da izmenite izabran.";
                return;
            }

            try
            {
                connection.Open();

                int radnikID = int.Parse(GridView1.SelectedRow.Cells[1].Text);
                string imePrezime = TextBox1.Text;
                string radnoMesto = DropDownList1.SelectedItem.Value;
                int plata = int.Parse(TextBox3.Text);

                AzurirajRadnika(connection, imePrezime, radnoMesto, plata, radnikID);

                Response.Redirect("~/Admin/OrganizujPodatke.aspx", false);
            }
            catch (Exception ex)
            {
                Label4.Text = "Greska";
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                connection.Close();
            }
        }

        protected void AzurirajRadnika(SqlConnection connection, string imePrezime, string radnoMesto, int plata, int radnikID)
        {
            SqlParameter p1 = new SqlParameter();
            SqlParameter p2 = new SqlParameter();
            SqlParameter p3 = new SqlParameter();
            SqlParameter p4 = new SqlParameter();

            p1.Value = imePrezime;
            p2.Value = radnoMesto;
            p3.Value = plata;
            p4.Value = radnikID;

            p1.ParameterName = "imePrezime";
            p2.ParameterName = "radnoMesto";
            p3.ParameterName = "plata";
            p4.ParameterName = "radnikID";

            string upit = "UPDATE Radnici SET imePrezime = @imePrezime, radnoMesto = @radnoMesto, plata = @plata WHERE radnikID = @radnikID";
            SqlCommand command = new SqlCommand(upit, connection);
            command.Parameters.Add(p1);
            command.Parameters.Add(p2);
            command.Parameters.Add(p3);
            command.Parameters.Add(p4);
            command.ExecuteNonQuery();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            // unesi radno mesto
            if (TextBox2.Text == "")
            {
                Label7.Text = "Polje mora biti popunjeno.";
                return;
            }

            try
            {
                connection.Open();

                string nazivMesta = TextBox2.Text;

                UnesiRadnoMesto(connection, nazivMesta);

                Response.Redirect("~/Admin/OrganizujPodatke.aspx", false);
            }
            catch (Exception ex)
            {
                Label7.Text = "Greska";
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                connection.Close();
            }
        }

        protected void UnesiRadnoMesto(SqlConnection connection, string nazivMesta)
        {
            SqlParameter p1 = new SqlParameter();

            p1.Value = nazivMesta;
            p1.ParameterName = "nazivMesta";

            string upit = "INSERT INTO RadnoMesto (nazivMesta) VALUES (@nazivMesta)";
            SqlCommand command = new SqlCommand(upit, connection);
            command.Parameters.Add(p1);
            command.ExecuteNonQuery();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            // obrisi radno mesto
            if (GridView2.SelectedRow == null)
            {
                Label7.Text = "Polje radnog mesta koje zelite da obrisete mora biti izabrano.";
                return;
            }

            try
            {
                connection.Open();

                int radnoMestoID = int.Parse(GridView2.SelectedRow.Cells[1].Text);

                ObrisiRadnoMesto(connection, radnoMestoID);

                Response.Redirect("~/Admin/OrganizujPodatke.aspx", false);
            }
            catch (Exception ex)
            {
                Label7.Text = "Greska";
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                connection.Close();
            }
        }

        protected void ObrisiRadnoMesto(SqlConnection connection, int radnoMestoID)
        {
            SqlParameter p1 = new SqlParameter();

            p1.Value = radnoMestoID;
            p1.ParameterName = "radnoMestoID";

            string upit = "DELETE FROM RadnoMesto WHERE radnoMestoID = @radnoMestoID";
            SqlCommand command = new SqlCommand(upit, connection);
            command.Parameters.Add(p1);
            command.ExecuteNonQuery();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            // azuriraj radno mesto

            if (TextBox2.Text == "" || GridView2.SelectedRow == null)
            {
                Label7.Text = "Polje mora biti popunjeno i radno mesto koje zelite da izmenite izabrano.";
                return;
            }

            try
            {
                connection.Open();

                int radnoMestoID = int.Parse(GridView2.SelectedRow.Cells[1].Text);
                string nazivMesta = TextBox2.Text;

                AzururajRadnoMesto(connection, nazivMesta, radnoMestoID);

                Response.Redirect("~/Admin/OrganizujPodatke.aspx", false);
            }
            catch (Exception ex)
            {
                Label7.Text = "Greska";
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                connection.Close();
            }
        }

        protected void AzururajRadnoMesto(SqlConnection connection, string nazivMesta, int radnoMestoID)
        {
            SqlParameter p1 = new SqlParameter();
            SqlParameter p2 = new SqlParameter();

            p1.Value = radnoMestoID;
            p1.ParameterName = "radnoMestoID";

            p2.Value = nazivMesta;
            p2.ParameterName = "nazivMesta";

            string upit = "UPDATE RadnoMesto SET nazivMesta = @nazivMesta WHERE radnoMestoID = @radnoMestoID";
            SqlCommand command = new SqlCommand(upit, connection);
            command.Parameters.Add(p1);
            command.Parameters.Add(p2);
            command.ExecuteNonQuery();
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView2.SelectedRow;

            if (row == null)
            {
                Label7.Text = "Izaberite red!";
                return;
            }

            TextBox2.Text = row.Cells[2].Text;
        }
    }
}