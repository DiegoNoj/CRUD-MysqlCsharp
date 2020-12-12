using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tieda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ClassCRUD crud = new ClassCRUD();
        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = crud.SelectProducto();
            foreach (DataRow item in dt.Rows)
            {
                dataGridView1.Rows.Add(new object[]{item[0].ToString(), item[1].ToString(), Convert.ToDecimal(item[2].ToString()), "Modificar",
                    "Eliminar"});
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool resultado = crud.InsertProducto(textBox1.Text, textBox2.Text, Convert.ToDecimal(textBox3.Text));
            if (resultado)
            {
                dataGridView1.Rows.Add(new object[]{textBox1.Text, textBox2.Text, Convert.ToDecimal(textBox3.Text), "Modificar",
                    "Eliminar"});
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "ColumnDgvModificar")
                    {
                        textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                        textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                        textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                    }

                    if (dataGridView1.Columns[e.ColumnIndex].Name == "ColumnDgvElimnar")
                    {
                        string upc = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                        bool resultado = crud.DeleteProducto(upc);
                        if (resultado)
                        {
                            dataGridView1.Rows.RemoveAt(e.RowIndex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool resultado = crud.UpdateProducto(textBox1.Text, textBox2.Text, Convert.ToDecimal(textBox3.Text));
            if (resultado)
            {
                dataGridView1.Rows.Add(new object[]{textBox1.Text, textBox2.Text, Convert.ToDecimal(textBox3.Text), "Modificar",
                    "Eliminar"});
            }
        }
    }
}
