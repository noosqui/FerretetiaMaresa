﻿using Dominio;
using System;
using System.Data;
using System.Media;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class RegistroProveedores : Form
    {
        private DOM_proveedor proveedor = new DOM_proveedor();
        private DOM_Validacion letrasNum = new DOM_Validacion();
        private ToolTip tt = new ToolTip();
        private bool estadobtna = false;
        private bool estadobtnb = false;
        private bool estadobtnc = false;
        private string estado = "";

        public RegistroProveedores()
        {
            InitializeComponent();

        }

        private void limpiar()
        {
            txtnombre.Clear();
            txtTelefono.Clear();
            txtcorreo.Clear();
            txtdireccion.Clear();
            txtciudad.Clear();
            txtregion.Clear();
            txtCodPost.Clear();
            txtPais.Clear();
            rbActivo.Checked = false;
            rbInactivo.Checked = false;
        }

        public void CargarDatos()
        {
            try
            {
                dgvProveedores.DataSource = proveedor.CargarDGVProveedores();
                dgvProveedores.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puedieron cargar los Datos" + ex);
            }
        }

        private void RegistroProveedores_Load(object sender, EventArgs e)
        {
            CargarDatos();

            txtcodigo.Text = dgvProveedores.CurrentRow.Cells[0].Value.ToString();
            txtnombre.Text = dgvProveedores.CurrentRow.Cells[1].Value.ToString();
            txtTelefono.Text = dgvProveedores.CurrentRow.Cells[2].Value.ToString();
            txtcorreo.Text = dgvProveedores.CurrentRow.Cells[3].Value.ToString();
            txtdireccion.Text = dgvProveedores.CurrentRow.Cells[4].Value.ToString();
            txtciudad.Text = dgvProveedores.CurrentRow.Cells[5].Value.ToString();
            txtregion.Text = dgvProveedores.CurrentRow.Cells[6].Value.ToString();
            txtCodPost.Text = dgvProveedores.CurrentRow.Cells[7].Value.ToString();
            txtPais.Text = dgvProveedores.CurrentRow.Cells[8].Value.ToString();
            estado = dgvProveedores.CurrentRow.Cells[9].Value.ToString();
            if (estado == "Activo")
            {
                rbActivo.Checked = true;
            }
            if (estado == "Inactivo")
            {
                rbInactivo.Checked = true;
            }
            dgvProveedores.Rows[0].Selected = true;


        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            estadobtna = true;
            estadobtnb = false;
            estadobtnc = false;

            dgvProveedores.Enabled = false;
            btnModificar.Visible = false;
            btnAgregar.Visible = false;
            btnEliminar.Visible = false;
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;

            txtnombre.Enabled = true;
            txtTelefono.Enabled = true;
            txtcorreo.Enabled = true;
            txtdireccion.Enabled = true;
            txtciudad.Enabled = true;
            txtregion.Enabled = true;
            txtCodPost.Enabled = true;
            txtPais.Enabled = true;
            txtbuscaridp.Enabled = false;
            txtbuscarnombre.Enabled = false;
            groupBox2.Enabled = false;
            rbActivo.Enabled = true;
            rbInactivo.Enabled = true;
            limpiar();

            Lim_ha encender = new Lim_ha();
            encender.Encender(this);

            Lim_ha Limpiar = new Lim_ha();
            Limpiar.Limpiar(this);

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (estadobtna == true)
            {
                if (txtnombre.Text == "" || txtTelefono.Text == "" || txtcorreo.Text == "" || txtdireccion.Text == "" ||
                        txtciudad.Text == "" || txtregion.Text == "" || txtCodPost.Text == "" || txtPais.Text == "")
                {
                    MessageBox.Show("Debe llenar todos los Campos");
                }
                else if (txtCodPost.TextLength < 5)
                {
                    MessageBox.Show("Codigo Postal Incompleto/Ingrese 5 Numeros");
                }
                else if (txtTelefono.TextLength < 8)
                {
                    MessageBox.Show("Numero de Telefono incompleto, debe Ingresar 8 numeros");
                }
                else
                {
                    try
                    {
                        if (rbActivo.Checked)
                        {
                            estado = "Activo";
                        }
                        if (rbInactivo.Checked)
                        {
                            estado = "Inactivo";
                        }
                        var dt = (DataTable)dgvProveedores.DataSource;
                        dt.DefaultView.RowFilter = string.Format("[Nombre] LIKE '{0}*'", txtnombre.Text);
                        if (dgvProveedores.Rows.Count > 0)
                        {
                            throw new Exception("Este proveedor ya existe");
                        }

                        dt.DefaultView.RowFilter = null;

                        proveedor.crear_proveedor(txtnombre.Text, txtTelefono.Text, txtcorreo.Text, txtdireccion.Text, txtciudad.Text,
                            txtregion.Text, txtCodPost.Text, txtPais.Text, estado);


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al Agregar Proveedor" + ex);
                    }
                }
            }
            if (estadobtnb == true)
            {
                if (txtnombre.Text == "" || txtTelefono.Text == "" || txtcorreo.Text == "" || txtdireccion.Text == "" ||
                    txtciudad.Text == "" || txtregion.Text == "" || txtCodPost.Text == "" || txtPais.Text == "")
                {
                    MessageBox.Show("Debe llenar todos los Campos");
                }
                else if (txtCodPost.TextLength < 5)
                {
                    MessageBox.Show("Codigo Postal Incompleto/Ingrese 5 Numeros");
                }
                else if (txtTelefono.TextLength < 8)
                {
                    MessageBox.Show("Numero de Telefono incompleto, debe Ingresar 8 numeros");
                }
                else
                {
                    try
                    {
                        if (rbActivo.Checked)
                        {
                            estado = "Activo";
                        }
                        if (rbInactivo.Checked)
                        {
                            estado = "Inactivo";
                        }

                        proveedor.editar_proveedor(txtcodigo.Text, txtnombre.Text, txtTelefono.Text, txtcorreo.Text, txtdireccion.Text, txtciudad.Text,
                            txtregion.Text, txtCodPost.Text, txtPais.Text, estado);


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error de Modificacion" + ex);
                    }
                }
            }
            if (estadobtnc == true)
            {
                try
                {
                    if (MessageBox.Show("¿Seguro que desea eliminar el proveedor?",
                        "ADVERTENCIA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        proveedor.eliminar_empleado(txtcodigo.Text);
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Error al Eliminar el proveedor");
                }
            }
            Lim_ha apagar = new Lim_ha();
            apagar.Apagar(this.panel1);
            btnAgregar.Visible = true;
            btnModificar.Visible = true;
            btnEliminar.Visible = true;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
            dgvProveedores.Enabled = true;
            rbActivo.Enabled = false;
            rbInactivo.Enabled = false;
            groupBox2.Enabled = true;
            txtbuscaridp.Enabled = true;
            txtbuscarnombre.Enabled = true;
            limpiar();

            RegistroProveedores_Load(null, null);
        }

        private void dgvProveedores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnAgregar.Visible = true;
            btnModificar.Visible = true;
            btnEliminar.Visible = true;
            btnGuardar.Text = "Guardar";
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
            dgvProveedores.Enabled = true;
            txtbuscaridp.Enabled = true;
            txtbuscarnombre.Enabled = true;
            groupBox2.Enabled = true;
            txtdireccion.Enabled = false;
            txtciudad.Enabled = false;
            txtregion.Enabled = false;
            txtCodPost.Enabled = false;
            txtPais.Enabled = false;
            txtnombre.Enabled = false;
            txtcorreo.Enabled = false;
            txtTelefono.Enabled = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            estadobtna = false;
            estadobtnb = true;
            estadobtnc = false;

            btnAgregar.Visible = false;
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;
            btnEliminar.Visible = false;
            btnModificar.Visible = false;
            rbActivo.Enabled = true;
            rbInactivo.Enabled = true;

            txtnombre.Enabled = true;
            txtTelefono.Enabled = true;
            txtcorreo.Enabled = true;
            txtbuscaridp.Enabled = false;
            txtbuscarnombre.Enabled = false;
            groupBox2.Enabled = false;
            rbActivo.Enabled = true;
            rbInactivo.Enabled = true;
            dgvProveedores.Enabled = false;
            txtdireccion.Enabled = true;
            txtciudad.Enabled = true;
            txtregion.Enabled = true;
            txtCodPost.Enabled = true;
            txtPais.Enabled = true;

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            btnAgregar.Visible = false;
            btnModificar.Visible = false;
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;
            btnEliminar.Visible = false;
            rbActivo.Enabled = true;
            rbInactivo.Enabled = true;

            estadobtna = false;
            estadobtnb = false;
            estadobtnc = true;


            txtnombre.Enabled = true;
            txtTelefono.Enabled = true;
            txtcorreo.Enabled = true;
            txtdireccion.Enabled = true;
            txtciudad.Enabled = true;
            txtregion.Enabled = true;
            txtCodPost.Enabled = true;
            txtPais.Enabled = true;
            rbActivo.Enabled = true;
            rbInactivo.Enabled = true;
            btnGuardar.Text = "Despedir";
        }

        private void rbfiltroactivos_CheckedChanged(object sender, EventArgs e)
        {
            var dt = (DataTable)dgvProveedores.DataSource;
            dt.DefaultView.RowFilter = string.Format("[Estado] LIKE '%{0}%'", "Activo");


            if (dt.DefaultView.Count < 1)
            {
                SystemSounds.Exclamation.Play();
                tt.Show("No se encontro parametros", this.rbfiltroactivos, 0, 25, 2000);
            }
        }

        private void rbfiltroinactivo_CheckedChanged(object sender, EventArgs e)
        {
            var dt = (DataTable)dgvProveedores.DataSource;
            dt.DefaultView.RowFilter = string.Format("[Estado] LIKE '%{0}%'", "Inactivo");


            if (dt.DefaultView.Count < 1)
            {
                SystemSounds.Exclamation.Play();
                tt.Show("No se encontro parametros", this.rbfiltroinactivo, 0, 25, 2000);
            }
        }

        private void rbfiltrotodos_CheckedChanged(object sender, EventArgs e)
        {
            var dt = (DataTable)dgvProveedores.DataSource;
            dt.DefaultView.RowFilter = null;

            if (dt.DefaultView.Count < 1)
            {
                SystemSounds.Exclamation.Play();
                tt.Show("No se encontro parametros", this.rbfiltrotodos, 0, 25, 2000);
            }
        }

        private void txtbuscaridp_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var dt = (DataTable)dgvProveedores.DataSource;
                if (txtbuscaridp.Text != "")
                {
                    dt.DefaultView.RowFilter = string.Format("[Id Proveedor] = {0}", txtbuscaridp.Text);
                }
                else
                {
                    dt.DefaultView.RowFilter = null;
                }
                dgvProveedores.Refresh();

                if (dt.DefaultView.Count < 1)
                {
                    SystemSounds.Exclamation.Play();
                    ToolTip tt = new ToolTip();
                    tt.Show("No se encontro parametros", this.txtbuscaridp, 0, 25, 3000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un Error" + ex);
            }
        }

        private void txtnombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            letrasNum.SoloLetras(e);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            letrasNum.SoloNumerosEnt(e);
        }

        private void txtcorreo_Leave(object sender, EventArgs e)
        {
            if (letrasNum.email(txtcorreo.Text) == false)
            {
                txtcorreo.Focus();
            }
        }

        private void txtciudad_KeyPress(object sender, KeyPressEventArgs e)
        {
            letrasNum.SoloLetras(e);
        }

        private void txtregion_KeyPress(object sender, KeyPressEventArgs e)
        {
            letrasNum.SoloLetras(e);
        }

        private void txtCodPost_KeyPress(object sender, KeyPressEventArgs e)
        {
            letrasNum.SoloNumerosEnt(e);
        }

        private void txtPais_KeyPress(object sender, KeyPressEventArgs e)
        {
            letrasNum.SoloLetras(e);
        }

        private void txtbuscaridp_KeyPress(object sender, KeyPressEventArgs e)
        {
            letrasNum.SoloNumerosEnt(e);
        }

        private void txtbuscarnombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            letrasNum.SoloLetras(e);
        }

        private void txtnombre_Enter(object sender, EventArgs e)
        {
            tt.Show("Ingrese Letras", this.txtnombre, 0, 25, 2000);
        }

        private void txtTelefono_Enter(object sender, EventArgs e)
        {
            tt.Show("Ingrese Numeros", this.txtTelefono, 0, 25, 2000);
        }

        private void txtcorreo_Enter(object sender, EventArgs e)
        {
            tt.Show("Ingrese Letras y/o Numeros", this.txtcorreo, 0, 25, 2000);
        }

        private void txtdireccion_Enter(object sender, EventArgs e)
        {
            tt.Show("Ingrese Letras y/o Numeros", this.txtdireccion, 0, 25, 2000);
        }

        private void txtbuscaridp_Enter(object sender, EventArgs e)
        {
            tt.Show("Ingrese Numeros", this.txtbuscaridp, 0, 25, 2000);
            txtbuscarnombre.Text = "";
        }

        private void txtbuscarnombre_Enter(object sender, EventArgs e)
        {
            tt.Show("Ingrese Letras", this.txtbuscarnombre, 0, 25, 2000);
            txtbuscaridp.Text = "";
        }

        private void txtciudad_Enter(object sender, EventArgs e)
        {
            tt.Show("Ingrese Letras", this.txtciudad, 0, 25, 2000);
        }

        private void txtregion_Enter(object sender, EventArgs e)
        {
            tt.Show("Ingrese Letras", this.txtregion, 0, 25, 2000);
        }

        private void txtCodPost_Enter(object sender, EventArgs e)
        {
            tt.Show("Ingrese Numeros", this.txtCodPost, 0, 25, 2000);
        }

        private void txtPais_Enter(object sender, EventArgs e)
        {
            tt.Show("Ingrese Letras", this.txtPais, 0, 25, 2000);
        }

        private void txtbuscarnombre_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                var dt = (DataTable)dgvProveedores.DataSource;
                dt.CaseSensitive = false;
                if (txtbuscarnombre.Text != "")
                {
                    dt.DefaultView.RowFilter = string.Format("[Nombre] LIKE '{0}*'", txtbuscarnombre.Text);
                }
                else
                {
                    dt.DefaultView.RowFilter = null;
                }

                dgvProveedores.Refresh();

                if (dt.DefaultView.Count < 1)
                {
                    SystemSounds.Exclamation.Play();
                    ToolTip tt = new ToolTip();
                    tt.Show("No se encontro parametros", this.txtbuscarnombre, 0, 25, 3000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un Error" + ex);
            }
        }

        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

