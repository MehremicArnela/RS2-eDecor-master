﻿using eDecor.Model.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eDecor.WinUI.Korisnici
{
    public partial class frmKorisnici : Form
    {
        private readonly APIService _korisniciService = new APIService("Korisnici");
        public frmKorisnici()
        {
            InitializeComponent();
        }
        private async void frmKorisnici_Load(object sender, EventArgs e)
        {
            var result = await _korisniciService.Get<List<Model.Korisnici>>(null);
            foreach (var item in result)
            {
                item.Uloge = "";
                foreach (var x in item.KorisniciUloge)
                {
                    item.Uloge += $"{x.Uloga.Naziv} ";
                }
            }
            dgvKorisnici.AutoGenerateColumns = false;
            dgvKorisnici.DataSource = result;
        }

        private async void btnPrikazi_Click(object sender, EventArgs e)
        {
            var result = await _korisniciService.Get<List<Model.Korisnici>>(new KorisniciSearchRequest()
            {
                Ime = txtIme.Text,
                Prezime = txtPrezime.Text,
                KorisnickoIme = txtKorisnickoIme.Text
            });

            foreach (var item in result)
            {
                item.Uloge = "";
                foreach (var x in item.KorisniciUloge)
                {
                    item.Uloge += $"{x.Uloga.Naziv} ";
                }
            }
            dgvKorisnici.AutoGenerateColumns = false;
            dgvKorisnici.DataSource = result;
        }

        private void dgvKorisnici_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var id = int.Parse(dgvKorisnici.SelectedRows[0].Cells[0].Value.ToString());
            frmKorisniciDetalji frm = new frmKorisniciDetalji(id);
            frm.ShowDialog();
        }
    }
}
