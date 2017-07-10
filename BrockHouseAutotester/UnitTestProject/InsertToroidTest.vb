Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class InsertToroidTest

    <TestMethod()> Public Sub StartInsertToroidTest()
        Dim workOrder As String = "306311"

        Dim core As Core = core.Instance
        core.CurrentWorkOrder_Refresh(workOrder)

        If core.CurrentWorkOrder Is Nothing Then
            System.Console.WriteLine("The given work order " + workOrder + " was NOT found in the database")
            Exit Sub
        Else
            System.Console.WriteLine("The given work order " + workOrder + " was found in the database")
        End If

        Dim tmp As DataElements.Sample = core.CurrentWorkOrder.Sample
        tmp.weight = 1
        tmp.SerialNo = 2001
        tmp.density = core.CurrentSettings.DensityDefault

        core.CurrentWorkOrder.Sample = tmp

        Dim newResult As New DataElements.Result
        Dim random As New Random
        newResult.Amps = random.NextDouble
        newResult.Date = System.DateTime.Now
        newResult.DuplicateTest = ""
        newResult.Grade = DataElements.Result.GradeT.Green
        newResult.PercentageActualJ = 99
        newResult.PercentageGreenLossLevel = 98
        newResult.Retest = 0 'random.Next(255)
        newResult.Serial = 2001
        newResult.State = DataElements.Result.StateT.Accept
        newResult.User = "UnitTest"
        newResult.Volts = random.NextDouble
        newResult.Watts = random.NextDouble
        newResult.Weight = random.NextDouble
        newResult.NumberOfTurns = random.Next()
        core.CurrentResult = newResult

        Dim done As Boolean = core.CurrentResultSave(DataElements.Result.StateT.Accept)
        Dim result As String = If(done, "Toroid result created", "error")
        Console.WriteLine(result)
    End Sub

End Class