Public Class Printer
    Private _printer As LabelPrinter.labelPrinter = Nothing

    Public Sub New()
        _printer = New LabelPrinter.labelPrinter
    End Sub

    Public Sub Print(ByVal workOrder As DataElements.WorkOrder, ByVal result As DataElements.Result, _
                     ByVal settings As Settings.Settings)
        'With result
        '    .AmpTurns = 5
        '    .Date = System.DateTime.Now
        '    .DuplicateTest = ""
        '    .Grade = DataElements.Result.GradeT.Red
        '    .Serial = "2"
        '    .Retest = 0
        '    .State = DataElements.Result.StateT.Accept
        '    .User = "Timo"
        '    .Watts = 4.58
        '    .Weight = 10.89
        'End With


        'With workOrder
        '    .WorkOrderNo = "123456"
        '    .CPI_CatalogueNo = "01041"
        '    .CustomerID = "ABBIN"
        '    .PurchaseOrderNo = "4500938085"
        '    .PartDescription = "783C374H28*"
        '    .Grade = "M4"
        '    .NominalInduction = 13.555
        'End With
        If settings.PrinterLayoutOption = Global.Settings.PrinterLayoutOptionT.Off Then Exit Sub

        Dim contentList As New List(Of LabelPrinter.Content)

        'Title
        Dim content As New LabelPrinter.Content(My.Resources.Controls.printTitle)
        content.IsBold = True
        content.IsHeadLine = True
        contentList.Add(content)

        'Cat no
        content = New LabelPrinter.Content(My.Resources.Controls.printCatNo, workOrder.CPI_CatalogueNo)
        contentList.Add(content)

        'Serial number
        content = New LabelPrinter.Content(My.Resources.Controls.printSerialNo, workOrder.WorkOrderNo + "-" + result.Serial + result.Test)
        contentList.Add(content)

        'Part desc
        content = New LabelPrinter.Content(My.Resources.Controls.printPartDescription, workOrder.PartDescription)
        contentList.Add(content)

        'Customer PO No
        content = New LabelPrinter.Content(My.Resources.Controls.printCustomerPoNo, workOrder.PurchaseOrderNo)
        contentList.Add(content)

        If workOrder.Sample.SampleType = DataElements.Sample.SampleTypeT.DgCore Then
            contentList.AddRange(GetDgCoreContent(workOrder, result, settings))
        Else
            contentList.AddRange(GetToroidContent(workOrder, result, settings))
        End If

        _printer.print(contentList)
    End Sub

    Private Function GetDgCoreContent(ByVal workOrder As DataElements.WorkOrder, _
                                      ByVal result As DataElements.Result, _
                                      ByVal settings As Settings.Settings) As List(Of LabelPrinter.Content)
        Dim contentList As New List(Of LabelPrinter.Content)

        'Loss
        Dim content As LabelPrinter.Content = New LabelPrinter.Content(My.Resources.Controls.printLoss, result.Watts.ToString("N2") + " Watts @ " + workOrder.NominalInduction.ToString("N2") + " T")
        contentList.Add(content)

        If Settings.PrinterLayoutOption = Global.Settings.PrinterLayoutOptionT.Full Then
            'Class
            content = New LabelPrinter.Content(My.Resources.Controls.printClass, result.Grade.ToString)
            Select Case result.Grade
                Case DataElements.Result.GradeT.Green : content.InhaltBG = Settings.ColorGreen
                Case DataElements.Result.GradeT.Red : content.InhaltBG = Settings.ColorRed
                Case DataElements.Result.GradeT.Rejected : content.InhaltBG = Settings.ColorRejected
                Case DataElements.Result.GradeT.Yellow : content.InhaltBG = Settings.ColorYellow
            End Select
            contentList.Add(content)

            'Current
            content = New LabelPrinter.Content(My.Resources.Controls.printExcitingCurrent, result.AmpTurns.ToString("N2") + " AmpTurns")
            contentList.Add(content)
        End If

        Return contentList
    End Function
    Private Function GetToroidContent(ByVal workOrder As DataElements.WorkOrder, _
                                      ByVal result As DataElements.Result, _
                                      ByVal settings As Settings.Settings) As List(Of LabelPrinter.Content)
        Dim contentList As New List(Of LabelPrinter.Content)

        'Amps
        Dim content As LabelPrinter.Content = New LabelPrinter.Content(My.Resources.Controls.sAmps, result.Amps.ToString("N4") + " A @ " + workOrder.NominalInduction.ToString("N2") + " T")
        contentList.Add(content)

        'Volts
        content = New LabelPrinter.Content(My.Resources.Controls.sVolts, result.Volts.ToString("N4") + " V")
        contentList.Add(content)

        If settings.PrinterLayoutOption = Global.Settings.PrinterLayoutOptionT.Full Then
            'Class
            content = New LabelPrinter.Content(My.Resources.Controls.printClass, result.Grade.ToString)
            Select Case result.Grade
                Case DataElements.Result.GradeT.Passed : content.InhaltBG = settings.ColorPassed
                Case DataElements.Result.GradeT.Rejected : content.InhaltBG = settings.ColorRejected
            End Select
            contentList.Add(content)
        End If

        Return contentList
    End Function

    Public Sub ShowPrintSetup()
        Dim newForm As New GuiPrinter
        newForm.ShowDialog()
    End Sub
End Class
