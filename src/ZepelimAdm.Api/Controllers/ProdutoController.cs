using Microsoft.AspNetCore.Mvc;
using System;
using ZepelimAdm.Business.Interfaces;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Api.Controllers
{
    [ApiController]
    [Route("api/v1/produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        [Route("findbyid")]
        public IActionResult FindById(int id)
        {
            try
            {
                var produtoencontrado = _produtoRepository.FindById(id);

                if (produtoencontrado.Result != null)
                {
                    return Ok(new
                    {
                        code = 200,
                        success = true,
                        return_date = DateTime.Now,
                        message = produtoencontrado.Result
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        code = 404,
                        success = false,
                        return_date = DateTime.Now,
                        message = "Nenhum produto encontrado."
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    code = 400,
                    success = false,
                    return_date = DateTime.Now,
                    message = e.Message
                });
            }
        }

        [HttpPost]
        [Route("save")]
        public IActionResult Save([FromBody] Produto produto)
        {
            try
            {
                if (produto == null)
                {
                    return NotFound(new
                    {
                        code = 404,
                        return_date = DateTime.Now,
                        success = false,
                        message = "Produto não encontrado."
                    });
                } 
                
                if (produto.Id == 0)
                {
                    var produtosalvo = _produtoRepository.Save(produto);

                    if (produtosalvo != null)
                    {
                        return Ok(new
                        {
                            code = 200,
                            success = true,
                            return_date = DateTime.Now,
                            message = produtosalvo
                        });
                    }
                    else
                    {
                        return BadRequest(new
                        {
                            code = 400,
                            success = false,
                            return_date = DateTime.Now,
                            message = "Ocorreu um problema ao tentar incluir o produto."
                        });
                    }
                }
                else
                {
                    var produtoencontrado = _produtoRepository.FindById(produto.Id);

                    if (produtoencontrado != null)
                    {
                        Produto produtoalterar = produtoencontrado.Result;

                        produtoalterar.Id = produto.Id;
                        produtoalterar.Descricao = produto.Descricao;

                        var produtoalterado = _produtoRepository.Update(produtoalterar);

                        if (produtoalterado != null)
                        {
                            return Ok(new
                            {
                                code = 200,
                                success = true,
                                return_date = DateTime.Now,
                                message = produtoalterado
                            });
                        }
                        else
                        {
                            return BadRequest(new
                            {
                                code = 400,
                                success = false,
                                return_date = DateTime.Now,
                                message = "Ocorreu um erro ao tentar alterar o produto"
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
                            message = "Código do produto não encontrado."
                        });
                    }
                }
            } 
            catch (Exception e)
            {
                return BadRequest(new
                {
                    code = 400,
                    success = false,
                    return_date = DateTime.Now,
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
                    var produtoencontrado = _produtoRepository.FindById(id);

                    if (produtoencontrado.Result != null)
                    {
                        _produtoRepository.RemoveById(id);                        
                        
                        return Ok(new
                        {
                            code = 200,
                            success = true,
                            return_date = DateTime.Now,
                            message = "Produto excluído com sucesso."
                        });
                    }
                    else
                    {
                        return NotFound(new
                        {
                            code = 404,
                            success = false,
                            return_date = DateTime.Now,
                            message = "Produto não encontrado."
                        });
                    }
                } else
                {
                    return BadRequest(new
                    {
                        code = 400,
                        return_date = DateTime.Now,
                        success = false,
                        message = "ID do produto não informado."
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    code = 400,
                    success = false,
                    return_date = DateTime.Now,
                    message = e.Message
                });
            }
        }
    }
}
