using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tieda
{
    class ClassCRUD
    {
        public bool InsertProducto(string _upc, string _desc, decimal _cantida)
        {
            try
            {
                using (OdbcConnection con = new OdbcConnection("FIL=MS Access;DSN=tieda"))
                {
                    OdbcCommand cmd = new OdbcCommand();
                    con.Open();
                    cmd.Connection = con;

                    #region Query
                    string query = @"INSERT INTO producto (Upc,Descripcion,Cantidad)VALUE(?,?,?);";
                    #endregion
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.Add("@Upc", OdbcType.VarChar).Value = _upc;
                    cmd.Parameters.Add("@Descripcion", OdbcType.VarChar).Value = _desc;
                    cmd.Parameters.Add("@Cantidad", OdbcType.Decimal).Value = _cantida;
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    con.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool UpdateProducto(string _upc, string _desc, decimal _cantidad)
        {
            try
            {
                using (OdbcConnection con = new OdbcConnection("FIL=MS Access;DSN=tieda"))
                {
                    OdbcCommand cmd = new OdbcCommand();
                    con.Open();
                    cmd.Connection = con;

                    #region Query
                    string query = @"UPDATE producto SET producto.Descripcion = ?, producto.Cantidad = ? WHERE producto.Upc = ?;";
                    #endregion
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.Add("@Descripcion", OdbcType.VarChar).Value = _desc;
                    cmd.Parameters.Add("@Cantidad", OdbcType.Decimal).Value = _cantidad;
                    cmd.Parameters.Add("@Upc", OdbcType.VarChar).Value = _upc;
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    con.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool DeleteProducto(string _upc)
        {
            try
            {
                using (OdbcConnection con = new OdbcConnection("FIL=MS Access;DSN=tieda"))
                {
                    OdbcCommand cmd = new OdbcCommand();
                    con.Open();
                    cmd.Connection = con;
                    #region Query
                    string query = @"DELETE FROM producto WHERE producto.Upc = ?;";
                    #endregion
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.Add("@Upc", OdbcType.VarChar).Value = _upc;
                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    con.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public DataTable SelectProducto()
        {
            DataTable dt = new DataTable();
            using (OdbcDataAdapter adapter = new OdbcDataAdapter("SELECT * FROM producto;", "FIL=MS Access;DSN=tieda"))
            {
                adapter.SelectCommand.CommandType = CommandType.Text;
                adapter.Fill(dt);
            }
            return dt;
        }
    }
}
