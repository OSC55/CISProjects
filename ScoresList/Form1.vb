Public Class Form1

    'Class level variables
    Dim scoreList As List(Of Integer)
    Dim scoreCount As Integer
    Dim scoreMax As Decimal
    Dim scoreMin As Decimal
    Dim scoreAverage As Decimal
    Dim strError As String


    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        'reset all errors
        strError = Nothing

        'validate all form
        validateForm()

        If String.IsNullOrEmpty(strError) Then
            'No errors go ahead and add the score to the list
            scoreList.Add(txtScore.Text)

            'Increment count class variable
            scoreCount += 1
            txtScoreCount.text = scoreCount

            'display averageScore
            txtScoreAverage.Text = getScoreAverage()

            'display minscore
            txtScoreMax.Text = getScoreMax()

            'display average score
            txtScoreMin.Text = getScoreMin()
        Else
            'there was an error. Display it to the user
            MessageBox.Show(strError)
        End If
    End Sub

    Private Function getScoreAverage() As Decimal
        'Calculate average

        Dim decTotal As Decimal = 0
        Dim decAverage As Decimal = 0

        For i As Integer = 0 To scoreList.Count - 1
            decTotal += scoreList(i)
        Next

        decAverage = decTotal / scoreCount

        Return decAverage

    End Function

    Private Function getScoremax() As Integer
        'Calculate Max

        Dim intMax As Integer = scoreList(0)

        For i As Integer = 0 To scoreList.Count - 1
            If scoreList(i) > intMax Then
                intMax = scoreList(i)
            End If
        Next

        Return intMax
    End Function

    Private Function getScoreMin() As Integer
        'Calculate Min

        Dim intMin As Integer = scoreList(0)

        For i As Integer = 0 To scoreList.Count - 1
            If scoreList(i) < intMin Then
                intMin = scoreList(i)
            End If
        Next

        Return intMin
    End Function

    Private Sub validateForm()
        'validate individual controls
        getValidScore(txtScore.Text)
    End Sub

    Private Sub getValidScore(input As String)
        Dim aScore As Integer
        Try
            'Make sure it's an Int
            aScore = Convert.ToInt32(input)

            'Make sure it's positive
            If (aScore <= 0) Then
                addError("Invalid score entered. Scores should be greater than 0")
            End If

        Catch ex As Exception
            'Not an int
            addError("Invalid score entered. Scores should be integers")
        End Try
    End Sub

    Private Sub addError(anError As String)
        'add errors to the list of errors
        If String.IsNullOrEmpty(strError) Then
            strError = "There was an error processing your request" & vbCrLf & vbCrLf
            strError += anError & vbCrLf
        Else
            strError += anError & vbCrLf
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        scoreList = New List(Of Integer)
    End Sub

    Private Sub btnList_Click(sender As Object, e As EventArgs) Handles btnList.Click
        'List Scores

        If scoreList.Count > 0 Then
            Dim strSummary = "Score list" & vbCrLf & vbCrLf

            For i As Integer = 0 To scoreList.Count - 1
                strSummary += scoreList(i) & vbCrLf
            Next

            MessageBox.Show(strSummary)

        Else
            MessageBox.Show("List of scores is empty. Please add scores")
        End If

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim aScore As Integer = InputBox("Enter score")

        If scoreList.Count = 0 Then
            MessageBox.Show("List of scores is empty. Please add scores")
            Exit Sub
        End If
        For i As Integer = 0 To scoreList.Count
            If aScore = scoreList(i) Then
                'Found a matching score
                'Need to remove from list

                scoreList.Remove(aScore)


                'clear the score
                txtScore.Text = ""

                'Increment count class variable
                scoreCount -= 1
                txtScoreCount.Text = scoreCount

                'display averageScore
                txtScoreAverage.Text = getScoreAverage()

                'display minscore
                txtScoreMax.Text = getScoremax()

                'display average score
                txtScoreMin.Text = getScoreMin()

                MessageBox.Show("Removed score " & aScore & " at index " & i)
                Exit For
            End If
        Next
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        'Clear List
        scoreList.Clear()

        'Clear form
        For Each ctrl In Me.Controls
            If TypeOf (ctrl) Is TextBox Then
                ctrl.text = ""
            End If
        Next

    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Dim aScore As Integer = InputBox("Enter score")

        If scoreList.Count = 0 Then
            MessageBox.Show("List of scores is empty. Please add scores")
            Exit Sub
        End If
        For i As Integer = 0 To scoreList.Count
            If aScore = scoreList(i) Then
                'Found  matching score

                MessageBox.Show("Found score " & aScore & " at index " & i)
                Exit For
            End If
        Next
    End Sub
End Class
