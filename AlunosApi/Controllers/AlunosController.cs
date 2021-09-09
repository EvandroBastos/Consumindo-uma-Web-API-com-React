using AlunosApi.Models;
using AlunosApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Produces("Application/json")]
    public class AlunosController : ControllerBase
    {
        private IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;

         }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();
                return Ok(alunos);
            }
            catch (Exception)
            {
                //return BadRequest("Request inválido!");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter Alunos");
            }

        }
        [HttpGet("AlunosPorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunosByNome([FromQuery] string nome)
        {
            try
            {
                var alunos = await _alunoService.GetAlunosByNome(nome);
                if (alunos == null)                
                    return NotFound($"Não Existem Alunos com o critério {nome}");                
                return Ok(alunos);
            }
            catch
            {
                return BadRequest("Request Invalido");
            }
        }
        
        [HttpGet("{id:int}", Name ="GetAluno")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            try
            {
                var alunos =await _alunoService.GetAluno(id);
                if (alunos == null)
                {
                    return NotFound($"Não Existem Alunos com o id = {id}");
                }
                return Ok(alunos);
            }
            catch
            {

                return BadRequest("Request Invalido");
            }
           

        }
        [HttpPost]
        public async Task<ActionResult>Create(Aluno aluno)
        {
            try
            {
                await _alunoService.CreateAluno(aluno);
                return CreatedAtRoute(nameof(GetAluno), new { id = aluno.AlunoId }, aluno);
            }
            catch
            {

                return BadRequest("Request Invalido");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id,[FromBody] Aluno aluno)
        {
            try
            {
                if(aluno.AlunoId == id)
                {
                    await _alunoService.UpdateAluno(aluno);
                    return Ok($"Aluno com id={id} foi atualizado com sucesso!");
                }else
                {
                    return BadRequest("Request Invalido");
                }

            }
            catch
            {
                return BadRequest("Request Invalido");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult>Delete(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if(aluno!= null)
                {
                    await _alunoService.DeleteAluno(aluno);
                    return Ok($"Aluno de id={id} Excluído!");
                }
                else
                {
                    return Ok($"Aluno de id={id} não encontrado!");
                }
            }
            catch 
            {
                return BadRequest("Request Invalido");
            }
        }


    }
}
