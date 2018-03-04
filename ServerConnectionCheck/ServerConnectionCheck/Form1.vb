Imports System.Net.NetworkInformation
Public Class Form1
    Dim serverIPs As String() = {"104.160.131.3", "104.160.141.3", "104.160.142.3", "104.160.156.1", "104.160.136.3"}

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        TextBox1.Text = serverIPs(ComboBox1.SelectedIndex)
    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Panel1.Enabled = False
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ComboBox1.Enabled = False
        NumericUpDown1.Enabled = False
        NumericUpDown2.Enabled = False
        Button1.Enabled = False
        Panel1.Enabled = True

        Dim pings As Decimal = NumericUpDown1.Value
        Dim latency As New List(Of Integer)
        Dim drops As Integer = 0
        Dim host As String = TextBox1.Text
        ProgressBar1.Maximum = pings
        For index As Integer = 0 To pings
            Dim pingreq As Ping = New Ping()
            Dim rep As PingReply = pingreq.Send(host, NumericUpDown2.Value)
            If rep.Status = IPStatus.Success Then
                latency.Add(rep.RoundtripTime)
            Else
                drops += 1
            End If

            ProgressBar1.Value = index

            If latency.Count > 0 Then
                Label9.Text = (Math.Round(latency.Average)).ToString & "ms"
            Else
                Label9.Text = ">500ms"
            End If

            If drops > 0 Then
                Label10.Text = (Math.Round(((1 - (drops / (index + 1))) * 100), 2)).ToString & "%"
            Else
                Label10.Text = "100%"
            End If

            Panel1.Update()
        Next

        ComboBox1.Enabled = True
        NumericUpDown1.Enabled = True
        NumericUpDown2.Enabled = True
        Button1.Enabled = True
        Panel1.Enabled = False
        ProgressBar1.Value = 0
    End Sub
End Class
