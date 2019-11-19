using System;
using System.Collections.Generic;
using System.Text;

namespace TestDrive.Models
{

    class ListagemVeiculos
    {

        public List<Veiculo> Veiculos { get; set; }
        public ListagemVeiculos()
        {

            this.Veiculos = new List<Veiculo>
            {
                new Veiculo{ Nome="Azera V6", Preco=60000},
                new Veiculo{ Nome="Fiesta", Preco=50000},
                new Veiculo{ Nome="HB20S", Preco=40000}
            };
        }
    }
}
