using MinhaApi.Models;
using MinhaApi.Repositories;
using MySqlConnector;

public class FornecedorRepository : IFornecedorRepository
{
    private readonly string _connectionString;

    public FornecedorRepository(IConfiguration config)
        => _connectionString = config.GetConnectionString("DefaultConnection")!;

    public IEnumerable<Fornecedor> GetAll()
    {
        var lista = new List<Fornecedor>();

        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = "SELECT id, nome, email, telefone, ativo FROM fornecedores";

        using var cmd = new MySqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Fornecedor
            {
                id = reader.GetInt32("id"),
                Nome = reader.GetString("nome"),
                Email = reader.GetString("email"),
                Telefone = reader.GetString("telefone"),
                Ativo = reader.GetBoolean("ativo")
            });
        }

        return lista;
    }

    public Fornecedor? GetById(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"SELECT id, nome, email, telefone, ativo
                       FROM fornecedores
                       WHERE id = @Id";

        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new Fornecedor
            {
                id = reader.GetInt32("id"),
                Nome = reader.GetString("nome"),
                Email = reader.GetString("email"),
                Telefone = reader.GetString("telefone"),
                Ativo = reader.GetBoolean("ativo")
            };
        }

        return null;
    }

    public void Add(Fornecedor fornecedor)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"INSERT INTO fornecedores
                       (nome, email, telefone, ativo)
                       VALUES
                       (@Nome, @Email, @Telefone, @Ativo);

                       SELECT LAST_INSERT_ID();";

        using var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Nome", fornecedor.Nome);
        cmd.Parameters.AddWithValue("@Email", fornecedor.Email);
        cmd.Parameters.AddWithValue("@Telefone", fornecedor.Telefone);
        cmd.Parameters.AddWithValue("@Ativo", fornecedor.Ativo);

        var idGerado = cmd.ExecuteScalar();

        fornecedor.id = Convert.ToInt32(idGerado);
    }

    public void Update(Fornecedor fornecedor)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"UPDATE fornecedores
                       SET nome = @Nome,
                           email = @Email,
                           telefone = @Telefone,
                           ativo = @Ativo
                       WHERE id = @Id";

        using var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Id", fornecedor.id);
        cmd.Parameters.AddWithValue("@Nome", fornecedor.Nome);
        cmd.Parameters.AddWithValue("@Email", fornecedor.Email);
        cmd.Parameters.AddWithValue("@Telefone", fornecedor.Telefone);
        cmd.Parameters.AddWithValue("@Ativo", fornecedor.Ativo);

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = "DELETE FROM fornecedores WHERE id = @Id";

        using var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Id", id);

        cmd.ExecuteNonQuery();
    }
}