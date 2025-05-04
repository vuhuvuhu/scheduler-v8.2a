' 🔄 ეს მოდული შეიცავს ყველა იმ ფუნქციას, რომელიც საჭიროა Google Sheets-თან მუშაობისთვის
' მონაცემების წამოღება, უნიკალური მნიშვნელობების გამოყოფა და კონკრეტული სვეტებიდან ინფორმაციის გამოტანა

Imports Google.Apis.Sheets.v4
Imports Google.Apis.Sheets.v4.Data

Public Module GoogleSheetsHelper

    ' 📥 აბრუნებს ფურცლიდან ყველა სტრიქონს (სათაურის გარეშე)
    Public Function GetSheetData(service As SheetsService, spreadsheetId As String, sheetName As String) As List(Of IList(Of Object))
        ' ვადგენთ სამუშაო დიაპაზონს — A1:Z
        Dim range As String = $"{sheetName}!A1:Z"
        Dim request = service.Spreadsheets.Values.Get(spreadsheetId, range)
        Dim response = request.Execute()
        Dim values = response.Values

        ' თუ ცარიელია, ვაბრუნებთ ცარიელ სიას
        If values Is Nothing Then
            Return New List(Of IList(Of Object))()
        End If

        ' გამოტოვებს სათაურის სტრიქონს და აბრუნებს დანარჩენს
        Return values.Skip(1).ToList()
    End Function

    ' 🔍 იღებს უნიკალურ მნიშვნელობებს კონკრეტული სვეტიდან
    Public Function GetUniqueColumnValues(service As SheetsService, spreadsheetId As String, sheetName As String, columnIndex As Integer) As List(Of String)
        Dim values As New HashSet(Of String)
        Dim allRows = GetSheetData(service, spreadsheetId, sheetName)

        For Each row In allRows
            If row.Count > columnIndex Then
                Dim val = row(columnIndex).ToString().Trim()
                If Not String.IsNullOrWhiteSpace(val) Then
                    values.Add(val)
                End If
            End If
        Next

        Return values.ToList()
    End Function

    ' 🧑‍⚕️ აბრუნებს ყველა უნიკალურ თერაპევტის სახელს (DB-Personal ფურცლიდან B სვეტი)
    Public Function GetTherapistNames(service As SheetsService, spreadsheetId As String) As List(Of String)
        Return GetUniqueColumnValues(service, spreadsheetId, "DB-Personal", 1)
    End Function

    ' 💬 აბრუნებს ყველა უნიკალურ თერაპიის სახელს (DB-Therapy ფურცლიდან B სვეტი)
    Public Function GetTherapyNames(service As SheetsService, spreadsheetId As String) As List(Of String)
        Return GetUniqueColumnValues(service, spreadsheetId, "DB-Therapy", 1)
    End Function

    ' 💰 აბრუნებს ყველა უნიკალურ დაფინანსების სახელს (DB-Program ფურცლიდან B სვეტი)
    Public Function GetFundingNames(service As SheetsService, spreadsheetId As String) As List(Of String)
        Return GetUniqueColumnValues(service, spreadsheetId, "DB-Program", 1)
    End Function

End Module
