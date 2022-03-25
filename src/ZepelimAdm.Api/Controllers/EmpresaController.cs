using Microsoft.AspNetCore.Mvc;
using System;
using ZepelimAdm.Business.Interfaces;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Api.Controllers
{
    [ApiController]
    [Route("api/v1/empresa")]
    public class EmpresaController : ControllerBase
    {
        public readonly IEmpresaRepository _empresaRepository;
        public readonly IUserRepository _userRepository;
        public readonly IEmpresaUserRepository _empresaUserRepository;
        public readonly IProdutoRepository _produtoRepository;
        public readonly IEmpresaProdutoRepository _empresaProdutoRepository;

        public EmpresaController(IEmpresaRepository empresaRepository, IUserRepository userRepository, IEmpresaUserRepository empresaUserRepository, IProdutoRepository produtoRepository, IEmpresaProdutoRepository empresaProdutoRepository)
        {
            _empresaRepository = empresaRepository;
            _userRepository = userRepository;
            _empresaUserRepository = empresaUserRepository;
            _produtoRepository = produtoRepository;
            _empresaProdutoRepository = empresaProdutoRepository;
        }

        [HttpGet]
        [Route("findbyid")]
        public IActionResult FindById(int id)
        {
            try
            {
                var empresaencontrada = _empresaRepository.FindById(id);

                if (empresaencontrada.Result != null)
                {
                    return Ok(new
                    {
                        code = 200,
                        success = true,
                        return_date = DateTime.Now,
                        message = empresaencontrada.Result
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        code = 404,
                        return_date = DateTime.Now,
                        success = false,
                        message = "Empresa não informada."
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
        public IActionResult save([FromBody] Empresa empresa)
        {
            try
            {
                if (empresa == null)
                {
                    return NotFound(new
                    {
                        code = 404,
                        return_date = DateTime.Now,
                        success = false,
                        message = "Empresa não informada."
                    });
                }

                if (empresa.Id > 0)
                {
                    var empresaencontrada = _empresaRepository.FindById(empresa.Id);

                    if (empresaencontrada.Result == null)
                    {
                        return NotFound(new
                        {
                            code = 404,
                            success = false,
                            return_date = DateTime.Now,
                            message = "Empresa não encontrada."
                        });
                    }
                    else
                    {
                        Empresa empresaalterar = empresaencontrada.Result;

                        empresaalterar.Descricao = empresa.Descricao;
                        empresaalterar.Documento = empresa.Documento;
                        empresaalterar.ConnectionString = empresa.ConnectionString;

                        var empresasalva = _empresaRepository.Update(empresaalterar);

                        if (empresasalva != null)
                        {
                            return Ok(new
                            {
                                code = 200,
                                success = true,
                                return_date = DateTime.Now,
                                message = empresasalva
                            });
                        }
                        else
                        {
                            return BadRequest(new
                            {
                                code = 400,
                                success = false,
                                return_date = DateTime.Now,
                                message = "Ocorreu um problema ao tentar alterar a empresa."
                            });
                        }
                    }
                }
                else
                {
                    var empresaencontrada = _empresaRepository.CheckIsUnique(empresa.Documento);

                    if (empresaencontrada.Result == null)
                    {
                        var empresasalva = _empresaRepository.Save(empresa);

                        if (empresasalva != null)
                        {
                            return Ok(new
                            {
                                code = 200,
                                success = true,
                                return_date = DateTime.Now,
                                message = empresasalva
                            });
                        }
                        else
                        {
                            return BadRequest(new
                            {
                                code = 400,
                                success = false,
                                return_date = DateTime.Now,
                                message = "Ocorreu um problema ao tentar incluir a empresa."
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
                            message = "Documento já utilizado em outro cadastro."
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
                    var empresaencontrada = _empresaRepository.FindById(id);

                    if (empresaencontrada.Result != null)
                    {
                        _empresaRepository.RemoveById(id);

                        return Ok(new
                        {
                            code = 200,
                            success = true,
                            return_date = DateTime.Now,
                            message = "Empresa excluída com sucesso."
                        });
                    }
                    else
                    {
                        return NotFound(new
                        {
                            code = 404,
                            success = false,
                            return_date = DateTime.Now,
                            message = "Empresa não encontrada."
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
                        message = "ID da empresa não informado."
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
        [Route("saveempuser")]
        public IActionResult saveempuser([FromBody] EmpresaUser empresa)
        {
            try
            {
                if (empresa == null)
                {
                    return NotFound(new
                    {
                        code = 404,
                        return_date = DateTime.Now,
                        success = false,
                        message = "Empresa X Usuário não informada."
                    });
                }

                if (empresa.EmpresaId > 0 )
                {
                    if (empresa.UsuarioId > 0)
                    {
                        var empresaencontrado = _empresaRepository.FindById(empresa.EmpresaId);
                        var usuarioencontrado = _userRepository.FindById(empresa.UsuarioId);

                        if (empresaencontrado != null)
                        {
                            if (usuarioencontrado != null)
                            {
                                var empresaxusuario = _empresaUserRepository.CheckIsUnique(empresa.EmpresaId, empresa.UsuarioId);

                                if (empresaxusuario != null)
                                {
                                    var registrosalvo = _empresaUserRepository.Save(empresa);

                                    if (registrosalvo != null)
                                    {
                                        return Ok(new
                                        {
                                            code = 200,
                                            return_date = DateTime.Now,
                                            success = false,
                                            message = registrosalvo
                                        });
                                    }
                                    else
                                    {
                                        return BadRequest(new
                                        {
                                            code = 400,
                                            return_date = DateTime.Now,
                                            success = false,
                                            message = "Ocorreu um erro ao tentar salvar o relacionamento Empresa X Usuário."
                                        });
                                    }
                                } else
                                {
                                    return BadRequest(new
                                    {
                                        code = 400,
                                        return_date = DateTime.Now,
                                        success = false,
                                        message = "O relacionamento Empresa X Usuário já existe."
                                    });
                                }
                            } else
                            {
                                return NotFound(new
                                {
                                    code = 404,
                                    return_date = DateTime.Now,
                                    success = false,
                                    message = "Id do usuário não encontrado."
                                });
                            }
                        } else
                        {
                            return NotFound(new
                            {
                                code = 404,
                                return_date = DateTime.Now,
                                success = false,
                                message = "Id da empresa não encontrado."
                            });
                        }
                    } else
                    {
                        return NotFound(new
                        {
                            code = 404,
                            return_date = DateTime.Now,
                            success = false,
                            message = "Empresa X Usuário não informada."
                        });
                    }
                } else
                {
                    return NotFound(new
                    {
                        code = 404,
                        return_date = DateTime.Now,
                        success = false,
                        message = "Empresa X Usuário não informada."
                    });
                }

            } catch (Exception e)
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
        [Route("saveempprd")]
        public IActionResult saveempprd([FromBody] EmpresaProduto empresa)
        {
            try
            {
                if (empresa == null)
                {
                    return NotFound(new
                    {
                        code = 404,
                        return_date = DateTime.Now,
                        success = false,
                        message = "Empresa X Produto não informada."
                    });
                }

                if (empresa.EmpresaId > 0)
                {
                    if (empresa.ProdutoId > 0)
                    {
                        var empresaencontrado = _empresaRepository.FindById(empresa.EmpresaId);
                        var produtoencontrado = _userRepository.FindById(empresa.ProdutoId);

                        if (empresaencontrado != null)
                        {
                            if (produtoencontrado != null)
                            {
                                var empresaxusuario = _empresaProdutoRepository.CheckIsUnique(empresa.EmpresaId, empresa.ProdutoId);

                                if (empresaxusuario != null)
                                {
                                    var registrosalvo = _empresaProdutoRepository.Save(empresa);

                                    if (registrosalvo != null)
                                    {
                                        return Ok(new
                                        {
                                            code = 200,
                                            return_date = DateTime.Now,
                                            success = false,
                                            message = registrosalvo
                                        });
                                    }
                                    else
                                    {
                                        return BadRequest(new
                                        {
                                            code = 400,
                                            return_date = DateTime.Now,
                                            success = false,
                                            message = "Ocorreu um erro ao tentar salvar o relacionamento Empresa X Produto."
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
                                        message = "O relacionamento Empresa X Produto já existe."
                                    });
                                }
                            }
                            else
                            {
                                return NotFound(new
                                {
                                    code = 404,
                                    return_date = DateTime.Now,
                                    success = false,
                                    message = "Id do produto não encontrado."
                                });
                            }
                        }
                        else
                        {
                            return NotFound(new
                            {
                                code = 404,
                                return_date = DateTime.Now,
                                success = false,
                                message = "Id da empresa não encontrado."
                            });
                        }
                    }
                    else
                    {
                        return NotFound(new
                        {
                            code = 404,
                            return_date = DateTime.Now,
                            success = false,
                            message = "Empresa X Produto não informada."
                        });
                    }
                }
                else
                {
                    return NotFound(new
                    {
                        code = 404,
                        return_date = DateTime.Now,
                        success = false,
                        message = "Empresa X Usuário não informada."
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
