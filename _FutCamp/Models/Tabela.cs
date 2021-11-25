using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CampeonatoFut.Models
{
    public class Tabela
    {
        [Display(Name = "Tabela Brasileirão 2021")]
        [Key()]
        public int IdTime { get; internal set; }
        public string Times { get; set; }
        public int Pontos { get; set; }
        public int Vencidos { get; set; }
        public int Empates { get; set; }
        public int Derrotas { get; set; }
        public int GolsFeitos { get; set; }
        public int GolsSofridos { get; set; }
        public int SaldodeGols { get; set; }

        public void SalvarTimes()
        {
            var db = new TabelaContext();
            db.TB_Brasileirao.Add(this);
            db.SaveChanges();
        }
       

    }
}
