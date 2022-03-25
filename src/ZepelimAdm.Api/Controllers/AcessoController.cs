using Microsoft.AspNetCore.Mvc;
using System;
using ZepelimAdm.Business.Interfaces;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Api.Controllers
{
    [ApiController]
    [Route("api/v1/acesso")]
    public class AcessoController : ControllerBase
    {
        public readonly IAcessoRepository _acessoRepository;
        public readonly IPaginaRepository _paginaRepository;
        public readonly IAcessoPaginaRepository _acessoPaginaRepository;
        public AcessoController(IAcessoRepository acessoRepository, IPaginaRepository paginaRepository, IAcessoPaginaRepository acessoPaginaRepository)
        {
            _acessoRepository = acessoRepository;
            _paginaRepository = paginaRepository;
            _acessoPaginaRepository = acessoPaginaRepository;
        }


        [HttpGet]
        [Route("findbyid")]
        public IActionResult findbyid(int id)
        {
            try
            {
                var acessoencontrada = _acessoRepository.FindById(id);

                if (acessoencontrada.Result != null)
                {
                    return Ok(new
                    {
                        code = 200,
                        success = true,
                        return_date = DateTime.Now,
                        message = acessoencontrada.Result
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        code = 404,
                        return_date = DateTime.Now,
                        success = false,
                        message = "Acesso não informado."
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
        public IActionResult save([FromBody] Acesso acesso)
        {
            try
            {
                if (acesso == null)
                {
                    return NotFound(new
                    {
                        code = 404,
                        return_date = DateTime.Now,
                        success = false,
                        message = "Acesso não informado."
                    });
                }

                if (acesso.Id > 0)
                {
                    var acessoencontrada = _acessoRepository.FindById(acesso.Id);

                    if (acessoencontrada.Result == null)
                    {
                        return NotFound(new
                        {
                            code = 404,
                            success = false,
                            return_date = DateTime.Now,
                            message = "Acesso não encontrado."
                        });
                    }
                    else
                    {
                        Acesso acessoalterar = acessoencontrada.Result;

                        acessoalterar.Descricao = acesso.Descricao;
                        acessoalterar.Status = acesso.Status;
                        acessoalterar.EmpresaId = acesso.EmpresaId;

                        var acessosalvo = _acessoRepository.Update(acessoalterar);

                        if (acessosalvo != null)
                        {
                            return Ok(new
                            {
                                code = 200,
                                success = true,
                                return_date = DateTime.Now,
                                message = acessosalvo
                            });
                        }
                        else
                        {
                            return BadRequest(new
                            {
                                code = 400,
                                success = false,
                                return_date = DateTime.Now,
                                message = "Ocorreu um problema ao tentar alterar o acesso."
                            });
                        }
                    }
                }
                else
                {
                    var acessoencontrada = _acessoRepository.CheckIsUnique(acesso.Descricao);

                    if (acessoencontrada.Result == null)
                    {
                        var acessosalvo = _acessoRepository.Save(acesso);

                        if (acessosalvo != null)
                        {
                            return Ok(new
                            {
                                code = 200,
                                success = true,
                                return_date = DateTime.Now,
                                message = acessosalvo
                            });
                        }
                        else
                        {
                            return BadRequest(new
                            {
                                code = 400,
                                success = false,
                                return_date = DateTime.Now,
                                message = "Ocorreu um problema ao tentar incluir o acesso."
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
                    var acessoencontrada = _acessoRepository.FindById(id);

                    if (acessoencontrada.Result != null)
                    {
                        _acessoRepository.RemoveById(id);

                        return Ok(new
                        {
                            code = 200,
                            success = true,
                            return_date = DateTime.Now,
                            message = "Acesso excluída com sucesso."
                        });
                    }
                    else
                    {
                        return NotFound(new
                        {
                            code = 404,
                            success = false,
                            return_date = DateTime.Now,
                            message = "Acesso não encontrado."
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
                        message = "ID do acesso não informado."
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
        [Route("saveaccesspage")]
        public IActionResult saveaccesspage([FromBody] AcessoPagina acesso)
        {
            try
            {
                if (acesso == null)
                {
                    return NotFound(new
                    {
                        code = 404,
                        return_date = DateTime.Now,
                        success = false,
                        message = "Acesso X página não informado."
                    });
                }

                if (acesso.AcessoId > 0)
                {
                    if (acesso.PaginaId > 0)
                    {
                        var acessoencontrado = _acessoRepository.FindById(acesso.AcessoId);
                        var paginaencontrada = _paginaRepository.FindById(acesso.PaginaId);

                        if (acessoencontrado != null)
                        {
                            if (paginaencontrada != null)
                            {
                                var paginaxacesso = _acessoPaginaRepository.CheckIsUnique(acesso.AcessoId, acesso.PaginaId);
                                
                                if (paginaxacesso != null)
                                {
                                    var registrosalvo = _acessoPaginaRepository.Save(acesso);

                                    if (registrosalvo != null)
                                    {
                                        return Ok(new
                                        {
                                            code = 200,
                                            success = true,
                                            return_date = DateTime.Now,
                                            message = registrosalvo
                                        });
                                    }
                                    else
                                    {
                                        return BadRequest(new
                                        {
                                            code = 400,
                                            success = false,
                                            return_date = DateTime.Now,
                                            message = "Erro ao tentar incluir o relacionamento Acesso X Página."
                                        });
                                    }
                                } else
                                {
                                    return BadRequest(new
                                    {
                                        code = 400,
                                        success = false,
                                        return_date = DateTime.Now,
                                        message = "O relacionamento Acesso X Página já existe."
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
                                    message = "Id da pagina não encontrado."
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
                                message = "Id da pagina não encontrado."
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
                            message = "Id do acesso não encontrado."
                        });
                    }
                }
                else
                {
                    return NotFound(new
                    {
                        code = 404,
                        success = false,
                        return_date = DateTime.Now,
                        message = "Id da página não encontrado."
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
