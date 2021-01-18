using eDecor.Model;
using eDecor.Model.Requests;
using eDecor.WinUI.Properties;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eDecor.WinUI
{
    public partial class frmPrijaviSe : Form
    {
        private readonly APIService _korisniciService = new APIService("Korisnici");
        private readonly APIService _ulogeService = new APIService("Uloge");
        public frmPrijaviSe()
        {
            InitializeComponent();
        }
        private async void btnPrijaviSe_Click(object sender, EventArgs e)
        {
            try
            {
                APIService.Username = txtKorisnickoIme.Text;
                APIService.Password = txtLozinka.Text;

                await _ulogeService.Get<dynamic>(null);//provjerimo da li postoj korisnik


                List<Model.Korisnici> list = await _korisniciService.Get<List<Model.Korisnici>>(new KorisniciSearchRequest() { KorisnickoIme = APIService.Username });
                var item = list.Where(x => x.KorisnickoIme == APIService.Username).FirstOrDefault();

                if (item != null)
                {
                    var frm = new frmPocetna();
                    this.Hide();
                    frm.Show();
                }
                else { 
                    MessageBox.Show("Pogrešno korisničko ime ili lozinka!");
                }
            }
            catch (FlurlHttpException ex) {

                if (ex.Call.HttpStatus != System.Net.HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("Niste pokrenuli API!");
                }
            }
        }

        private void pbPrikazi_Click(object sender, EventArgs e)
        {
            txtLozinka.UseSystemPasswordChar = !txtLozinka.UseSystemPasswordChar;
            pbPrikazi.Image = txtLozinka.UseSystemPasswordChar ? Resources.outline_visibility_black_18dp : Resources.outline_visibility_off_black_18dp;
        }
    }
}
