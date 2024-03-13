Public Class Form1


    Public conex As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=inventario.accdb")
    Public com As New OleDb.OleDbCommand


    Function ConectarDB()
        Try
            conex.Open()
            com.Connection = conex
            com.CommandType = CommandType.Text
            Console.WriteLine("Conexion con la base de datos correcta.")

        Catch ex As Exception

            If Err.Number = 5 Then
                MsgBox("Error al conectar la base de datos." & Err.Description)
            End If

        End Try
    End Function


    Function limpiarTextBox()

    End Function
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConectarDB()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Application.Exit()
    End Sub

    Private Sub LimpiarMisTextBox()
        txtCodigo.Clear()
        txtNombre.Clear()
        txtPrecio.Clear()
        txtCategoria.Clear()
        txtVendidas.Clear()
        txtExistentes.Clear()
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Dim codigo, nombre, Precio, categoria, vendidas, existentes As String


        codigo = txtCodigo.Text
        nombre = txtNombre.Text
        Precio = txtPrecio.Text
        categoria = txtCategoria.Text
        vendidas = txtVendidas.Text
        existentes = txtExistentes.Text



        Dim sql As String = String.Format("INSERT INTO Inventario(Codigo, nombre_producto,precio,categoria,unid_vendidas,unidad_existentes) VALUES({0},'{1}',{2},'{3}',{4},{5})", codigo, nombre, Precio, categoria, vendidas, existentes)

        Console.WriteLine(sql)

        com.CommandText = sql

        Try
            com.ExecuteNonQuery()
            MsgBox("Registro " & codigo & " guardado Nombre: " & nombre & "")
            LimpiarMisTextBox()

        Catch ex As Exception
            MsgBox("¡UPS!, un error acaba de ocurrir " & Err.Description)
        End Try

    End Sub

    Private Sub btnActualiar_Click(sender As Object, e As EventArgs) Handles btnActualiar.Click
        Dim sqlActualizados As String = String.Format("SELECT * FROM Inventario")
        Dim adapt As New OleDb.OleDbDataAdapter(sqlActualizados, conex)
        Dim obtenerDatos As New DataSet
        adapt.Fill(obtenerDatos, "codigo")
        dataGridViewProductos.DataSource = obtenerDatos
        dataGridViewProductos.DataMember = "codigo"


    End Sub
End Class
