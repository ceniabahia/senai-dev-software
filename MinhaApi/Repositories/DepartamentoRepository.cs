using MinhaApi.Models;
using MinhaApi.Repositories;
using MySqlConnector;

public class DepartamentoRepository : IDepartamentoRepository
{
    private readonly string _connectionString;

    public DepartamentoRepository(IConfiguration config)
        => _connectionString = config.GetConnectionString("DefaultConnection")!;

    public IEnumerable<Departamento> GetAll()
    {
        var lista = new List<Departamento>();

        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = "SELECT id, nome, funcionario, email, telefone, ativo FROM departamento";

        using var cmd = new MySqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            lista.Add(new Departamento
            {
                id = reader.GetInt32("id"),
                Nome = reader.GetString("nome"),
                 Funcionario = reader.GetString("funcionario"),
                Email = reader.GetString("email"),
                Telefone = reader.GetString("telefone"),
                Ativo = reader.GetBoolean("ativo")
            });
        }

        return lista;
    }

    public Departamento? GetById(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"SELECT id, nome, funcionario, email, telefone, ativo
                       FROM derpatamento
                       WHERE id = @id";

        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            return new Departamento
            {
                id = reader.GetInt32("id"),
                Nome = reader.GetString("nome"),
                Funcionario = reader.GetString("funcionario"),
                Email = reader.GetString("email"),
                Telefone = reader.GetString("telefone"),
                Ativo = reader.GetBoolean("ativo")
            };
        }

        return null;
    }

    public void Add(Departamento departamento)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"INSERT INTO departamento
                       (nome, funcionario, email, telefone, ativo)
                       VALUES
                       (@Nome, @Funcionario, @Email, @Telefone, @Ativo);

                       SELECT LAST_INSERT_ID();";

        using var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Nome", departamento.Nome);
        cmd.Parameters.AddWithValue("@Funcionario", departamento.Funcionario);
        cmd.Parameters.AddWithValue("@Email", departamento.Email);
        cmd.Parameters.AddWithValue("@Telefone", departamento.Telefone);
        cmd.Parameters.AddWithValue("@Ativo", departamento.Ativo);

        var idGerado = cmd.ExecuteScalar();

        departamento.id = Convert.ToInt32(idGerado);
    }

    public void Update(Departamento departamento)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = @"UPDATE departamento
                       SET nome = @Nome,
                        funcionario = @Funcionario,
                           email = @Email,
                           telefone = @Telefone,
                           ativo = @Ativo
                       WHERE id = @Id";

        using var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Id", departamento.id);
        cmd.Parameters.AddWithValue("@Nome", departamento.Nome);
        cmd.Parameters.AddWithValue("@Funcionario", departamento.Funcionario);
        cmd.Parameters.AddWithValue("@Email", departamento.Email);
        cmd.Parameters.AddWithValue("@Telefone", departamento.Telefone);
        cmd.Parameters.AddWithValue("@Ativo", departamento.Ativo);

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string sql = "DELETE FROM departamento WHERE id = @Id";

        using var cmd = new MySqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Id", id);

        cmd.ExecuteNonQuery();
    }
}