using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace APPDB.Models.Helpers {
    public class ProdutoHelperCRUD {
        private string _conexaoBD;

        public ProdutoHelperCRUD(string conexao) {
            _conexaoBD = conexao;
        }

        public List<Produto> list(Produto.TipoEstado estadoAVer) {

            List<Produto> listaSaida = new List<Produto>();

            DataTable dtProdutos = new DataTable();
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlConnection conexao = new SqlConnection(_conexaoBD);
            SqlCommand comando = new SqlCommand();

            comando.Connection = conexao;
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM tProduto";
            //comando.CommandText = "SELECT * FROM tProduto WHERE estado = @estado";
            //comando.Parameters.AddWithValue("@estado", estadoAVer);
            telefone.SelectCommand = comando;
            telefone.Fill(dtProdutos);
            conexao.Close();
            conexao.Dispose();
            //Transformar um conjunto de registos num conjunto de objetos
            foreach (DataRow linha in dtProdutos.Rows) {
                Produto produto = new Produto();
                produto.Id = Guid.Parse(linha["uid"].ToString());
                produto.Designacao = linha["designacao"].ToString();
                produto.PUnitario = Convert.ToDecimal(linha["pUnitario"]);
                produto.StkAtual = Convert.ToDecimal(linha["stkAtual"]);
                produto.Estado = (Produto.TipoEstado)Convert.ToInt32(linha["estado"]);
                listaSaida.Add(produto);
            }
            return listaSaida;
        }

        public Guid insert(Produto produto) {
            Guid idReturned = Guid.Empty;
            if (produto.Id == Guid.Empty) {
                produto.Id = Guid.NewGuid();
                try {
                    SqlConnection conexao = new SqlConnection(_conexaoBD);
                    SqlCommand comando = new SqlCommand();
                    comando.Connection = conexao;
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = "INSERT INTO tProduto (uid, designacao, pUnitario, stkAtual, estado) " +
                                            " VALUES (@uid, @designacao, @pUnitario, @stkAtual, @estado)";
                    comando.Parameters.AddWithValue("@uid", produto.Id);
                    comando.Parameters.AddWithValue("@designacao", produto.Designacao);
                    comando.Parameters.AddWithValue("@pUnitario", produto.PUnitario);
                    comando.Parameters.AddWithValue("@stkAtual", produto.StkAtual);
                    comando.Parameters.AddWithValue("@estado", produto.Estado);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    conexao.Close();
                    conexao.Dispose();
                    idReturned = produto.Id;
                }
                catch (Exception ex) {
                    string erro = ex.Message;
                    idReturned = Guid.Empty;
                }
            }
            return idReturned;
        }

        public Produto read(string idAPesquisar) {

            Produto produtoSaida = new Produto();

            DataTable dtProdutos = new DataTable();
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlConnection conexao = new SqlConnection(_conexaoBD);
            SqlCommand comando = new SqlCommand();

            comando.Connection = conexao;
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM tProduto WHERE uid = @uid";
            comando.Parameters.AddWithValue("@uid", idAPesquisar);
            telefone.SelectCommand = comando;
            telefone.Fill(dtProdutos);
            conexao.Close();
            conexao.Dispose();
            if (dtProdutos.Rows.Count == 1) {
                DataRow linha = dtProdutos.Rows[0];     //e unica
                produtoSaida.Id = Guid.Parse(linha["uid"].ToString());
                produtoSaida.Designacao = linha["designacao"].ToString();
                produtoSaida.PUnitario = Convert.ToDecimal(linha["pUnitario"]);
                produtoSaida.StkAtual = Convert.ToDecimal(linha["stkAtual"]);
                produtoSaida.Estado = (Produto.TipoEstado)Convert.ToInt32(linha["estado"]);
            }
            return produtoSaida;        //ou vem da BD ou é um novo pq o id não existe
        }


        public Guid update(Produto produto) {
            Guid idReturned = Guid.Empty;
            if (produto.Id != Guid.Empty) {

                try {
                    SqlConnection conexao = new SqlConnection(_conexaoBD);
                    SqlCommand comando = new SqlCommand();
                    comando.Connection = conexao;
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = "UPDATE tProduto SET designacao = @designacao, " + 
                                            " pUnitario = @pUnitario, stkAtual = @stkAtual, estado = @estado " +
                                            " WHERE uid = @uid";
                    comando.Parameters.AddWithValue("@uid", produto.Id);
                    comando.Parameters.AddWithValue("@designacao", produto.Designacao);
                    comando.Parameters.AddWithValue("@pUnitario", produto.PUnitario);
                    comando.Parameters.AddWithValue("@stkAtual", produto.StkAtual);
                    comando.Parameters.AddWithValue("@estado", produto.Estado);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    conexao.Close();
                    conexao.Dispose();
                    idReturned = produto.Id;
                }
                catch {
                    idReturned = Guid.Empty;
                }
            }
            return idReturned;
        }

    }
}
