using Microsoft.AspNetCore.Mvc;
using System;
using ZepelimAdm.Business.Interfaces;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Api.Controllers
{
    [ApiController]
    [Route("api/v1/pagina")]
    public class PaginaController : ControllerBase
    {
        public readonly IPaginaRepository _paginaRepository;
        public PaginaController(IPaginaRepository paginaRepository)
        {
            _paginaRepository = paginaRepository;
        }

        [HttpGet]
        [Route("findbyid")]
        public IActionResult FindById(int id)
        {
            try
            {
                var paginaencontrada = _paginaRepository.FindById(id);

                if (paginaencontrada.Result != null)
                {
                    return Ok(new
                    {
                        code = 200,
                        success = true,
                        return_date = DateTime.Now,
                        message = paginaencontrada.Result
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        code = 404,
                        return_date = DateTime.Now,
                        success = false,
                        message = "Página não informada."
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    code = 400,
                    return_date = DateTime.Now,
                    success = false,
                    message = e.Message
                });
            }
        }

        [HttpPost]
        [Route("save")]
        public IActionResult Save([FromBody] Pagina pagina)
        {
            try
            {
                if (pagina == null)
                {
                    return NotFound(new
                    {
                        code = 404,
                        return_date = DateTime.Now,
                        success = false,
                        message = "Empresa não informada."
                    });
                }

                if (pagina.Id > 0)
                {
                    var paginaencontrada = _paginaRepository.FindById(pagina.Id);

                    if (paginaencontrada.Result == null)
                    {
                        return NotFound(new
                        {
                            code = 404,
                            success = false,
                            return_date = DateTime.Now,
                            message = "Página não encontrada."
                        });
                    }
                    else
                    {
                        Pagina paginaalterar = paginaencontrada.Result;

                        paginaalterar.Descricao = pagina.Descricao;
                        paginaalterar.URL = pagina.URL;
                        paginaalterar.Ordem = pagina.Ordem;
                        paginaalterar.Icone = pagina.Icone;
                        paginaalterar.PaginaPaiId = pagina.PaginaPaiId;
                        paginaalterar.Status = pagina.Status;

                        var paginasalva = _paginaRepository.Update(paginaalterar);

                        if (paginasalva != null)
                        {
                            return Ok(new
                            {
                                code = 200,
                                success = true,
                                return_date = DateTime.Now,
                                message = paginasalva
                            });
                        }
                        else
                        {
                            return BadRequest(new
                            {
                                code = 400,
                                success = false,
                                return_date = DateTime.Now,
                                message = "Ocorreu um problema ao tentar alterar a página."
                            });
                        }
                    }
                }
                else
                {
                    var paginaencontrada = _paginaRepository.CheckIsUnique(pagina.Descricao);

                    if (paginaencontrada.Result == null)
                    {
                        var paginasalva = _paginaRepository.Save(pagina);

                        if (paginasalva != null)
                        {
                            return Ok(new
                            {
                                code = 200,
                                success = true,
                                return_date = DateTime.Now,
                                message = paginasalva
                            });
                        }
                        else
                        {
                            return BadRequest(new
                            {
                                code = 400,
                                success = false,
                                return_date = DateTime.Now,
                                message = "Ocorreu um problema ao tentar incluir a página."
                            });
                        }
                    }
                    else
                    {
                        return BadRequest(new
                        {
                            code = 400,
                            success = false,
                            return_date = DateTime.Now,
                            message = "Descrição já utilizado em outro cadastro."
                        });
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    code = 400,
                    return_date = DateTime.Now,
                    success = false,
                    message = e.Message
                });
            }
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    var paginaencontrada = _paginaRepository.FindById(id);

                    if (paginaencontrada.Result != null)
                    {
                        _paginaRepository.RemoveById(id);

                        return Ok(new
                        {
                            code = 200,
                            success = true,
                            return_date = DateTime.Now,
                            message = "Página excluída com sucesso."
                        });
                    }
                    else
                    {
                        return NotFound(new
                        {
                            code = 404,
                            success = false,
                            return_date = DateTime.Now,
                            message = "Página não encontrada."
                        });
                    }
                }
                else
                {
                    return BadRequest(new
                    {
                        code = 400,
                        return_date = DateTime.Now,
                        success = false,
                        message = "ID da Página não informado."
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    code = 400,
                    return_date = DateTime.Now,
                    success = false,
                    message = e.Message
                });
            }
        }
    }
}
