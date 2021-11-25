using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampeonatoFut.Models;
using Microsoft.EntityFrameworkCore;

namespace CampeonatoFut.Controllers
{
    public class Brasileirao : Controller
    {
        Tabela tabela;
        public Brasileirao()
        {
            tabela = new Tabela();
        }
        //Listar minha tabela.
        public JsonResult Index()
        {

            var context = new TabelaContext().TB_Brasileirao.ToList();


            context = context.OrderByDescending(x => x.Pontos)
                .ThenByDescending(x => x.Vencidos)
                .ThenByDescending(x => x.GolsFeitos)
                .ThenByDescending(x => x.SaldodeGols).ToList();


            return Json(context);
        }
        // metodo de listar times para confronto direto.
        public JsonResult GetTeamsChampionship()
        {
            var context = new TabelaContext().TB_Brasileirao.ToList();
            context = context.OrderBy(x => x.IdTime).ToList();

            var times = context.Select(x => new
            {
                Nome = x.Times,
                IdTime = x.IdTime
            }).ToList();

            return Json(times);
        }
        //metodo de input db com os dados dos jogos.
        [HttpPost]
        public void SaveMatchData( int host, int hostGoals, int foreign, int foreignGoals)
        {
            var hostObj = new TabelaContext().TB_Brasileirao.Where(x =>x.IdTime == host).FirstOrDefault();
            hostObj.GolsFeitos += hostGoals;
            hostObj.GolsSofridos += foreignGoals;
            hostObj.SaldodeGols = hostObj.GolsFeitos - hostObj.GolsSofridos;


            var foreignObj = new TabelaContext().TB_Brasileirao.Where(x => x.IdTime == foreign).FirstOrDefault();
            foreignObj.GolsFeitos += foreignGoals;
            foreignObj.GolsSofridos += hostGoals;
            foreignObj.SaldodeGols = foreignObj.GolsFeitos - foreignObj.GolsSofridos;

            if (hostGoals > foreignGoals)
            {
                hostObj.Vencidos += 1;
                foreignObj.Derrotas += 1;
                hostObj.Pontos += 3;
            }

            if (foreignGoals > hostGoals)
            {
                    foreignObj.Vencidos += 1;
                    hostObj.Derrotas += 1;
                    foreignObj.Pontos += 3;
            }

            if(hostGoals == foreignGoals)
            {
                hostObj.Empates += 1;
                foreignObj.Empates += 1;
                hostObj.Pontos += 1;
                foreignObj.Pontos += 1;
            }
     
            var context = new TabelaContext();
            context.TB_Brasileirao.Add(hostObj);
            context.TB_Brasileirao.Add(foreignObj);            
            context.Entry(hostObj).State = EntityState.Modified;
            context.Entry(foreignObj).State = EntityState.Modified;
            context.SaveChanges();


        }
        

    }
}
