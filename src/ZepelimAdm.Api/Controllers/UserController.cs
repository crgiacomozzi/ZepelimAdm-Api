using Microsoft.AspNetCore.Mvc;
using System;
using ZepelimAdm.Business.Interfaces;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Api.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("findbyid")]
        public IActionResult FindById(int id)
        {
            try
            {
                var usuarioencontrado = _userRepository.FindById(id);

                if (usuarioencontrado.Result != null)
                {
                    return Ok(new
                    {
                        code = 200,
                        success = true,
                        return_date = DateTime.Now,
                        message = usuarioencontrado.Result
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        code = 404,
                        return_date = DateTime.Now,
                        success = false,
                        message = "Usuário não informado."
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
        public IActionResult Save([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return NotFound(new
                    {
                        code = 404,
                        return_date = DateTime.Now,
                        success = false,
                        message = "Usuário não informado."
                    });
                }

                if (user.Id > 0)
                {
                    var usuarioencontrado = _userRepository.FindById(user.Id);

                    if (usuarioencontrado.Result == null)
                    {
                        return NotFound(new
                        {
                            code = 404,
                            success = false,
                            return_date = DateTime.Now,
                            message = "Usuário não encontrado."
                        });
                    }
                    else
                    {
                        User usuarioalterar = usuarioencontrado.Result;

                        usuarioalterar.Nome = user.Nome;
                        usuarioalterar.Email = user.Email;
                        usuarioalterar.Role = user.Role;

                        var usuariosalvo = _userRepository.Update(usuarioalterar);

                        if (usuariosalvo != null)
                        {
                            return Ok(new
                            {
                                code = 200,
                                success = true,
                                return_date = DateTime.Now,
                                message = usuariosalvo
                            });
                        }
                        else
                        {
                            return BadRequest(new
                            {
                                code = 400,
                                success = false,
                                return_date = DateTime.Now,
                                message = "Ocorreu um problema ao tentar alterar o usuário."
                            });
                        }
                    }
                }
                else
                {
                    var usuarioencontrado = _userRepository.CheckIsUnique(user.Email);

                    if (usuarioencontrado.Result == null)
                    {
                        var usuariosalvo = _userRepository.Save(user);

                        if (usuariosalvo != null)
                        {
                            return Ok(new
                            {
                                code = 200,
                                success = true,
                                return_date = DateTime.Now,
                                message = usuariosalvo
                            });
                        }
                        else
                        {
                            return BadRequest(new
                            {
                                code = 400,
                                success = false,
                                return_date = DateTime.Now,
                                message = "Ocorreu um problema ao tentar incluir o usuário."
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
                            message = "E-mail já utilizado em outro cadastro."
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
                    var usuarioencontrado = _userRepository.FindById(id);

                    if (usuarioencontrado.Result != null)
                    {
                        _userRepository.RemoveById(id);

                        return Ok(new
                        {
                            code = 200,
                            success = true,
                            return_date = DateTime.Now,
                            message = "Usuário excluído com sucesso."
                        });
                    }
                    else
                    {
                        return NotFound(new
                        {
                            code = 404,
                            success = false,
                            return_date = DateTime.Now,
                            message = "Usuário não encontrado."
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
                        message = "ID do usuário não informado."
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
