﻿Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports DataElements

Namespace DataExchange
    Partial Class Level2
        Private Class CogentDB

#Region "DB connection string and basic procedures"
            Private Const ConnectionStringBMT = "Data Source=testserver;Initial Catalog=Microsoft.SqlServer.Smo;User ID=sa;Password=root;"
            'Private Const ConnectionStringCogent = "Data Source=121COGENTS9K;Initial Catalog=CPI;User ID=SYSADM;Password=COPOW05"
            Private Shared ConnectionStringCogent As String = "Data Source=" + Core.Instance.CurrentSettings.DataSource +
                                                       ";Initial Catalog=CPI;User ID=" + Core.Instance.CurrentSettings.UserID +
                                                       ";Password=" + Core.Instance.CurrentSettings.Password + ";"

            Private Shared Function ExecuteDataTable(sql As String) As DataTable


                Dim Connection As New SqlConnection(ConnectionStringCogent)
                Dim DataAdapter As New SqlDataAdapter(sql, Connection)
                Dim DataTable As New DataTable()

                Try

                    DataAdapter.Fill(DataTable)
                    Return DataTable
                Catch ex As SqlException
                    Throw ex
                Finally
                    DataAdapter.Dispose()
                    If Connection.State <> ConnectionState.Closed Then
                        Connection.Close()
                    End If
                    Connection.Dispose()
                End Try

            End Function
            Friend Shared Function ExecuteNoN(sql As String) As [Boolean]
                Dim raus As [Boolean] = False
                Dim connection As New SqlConnection(ConnectionStringCogent)


                Try

                    Using connection
                        Dim command As New SqlCommand(sql, connection)
                        command.Connection.Open()

                        If command.ExecuteNonQuery() <> -1 Then
                            raus = True
                        End If
                    End Using
                Catch ex As SqlException
                    raus = False
                    Throw ex
                Finally
                    If connection.State <> ConnectionState.Closed Then
                        connection.Close()
                    End If
                    connection.Dispose()
                End Try
                Return raus
            End Function
#End Region

#Region "Basic RequestWorkOrder, RequestResults InsertResults"
            Private Const WORKORDER_UDF_LAYOUT_ID_Reg_Cores As String = "Part: Reg Cores"
            Private Const WORKORDER_UDF_LAYOUT_ID_Toroids As String = "Part: Toroids"
            Public Shared Function RequestWorkOrder(ByVal workOrderNo As String, ByRef workOrder As WorkOrder) As Boolean
                Dim cwoObj As CogentWorkOrderComplex = Nothing
                Dim testType As String = ""

                If Not GetTestType(workOrderNo, testType) Then Return False

                If testType = WORKORDER_UDF_LAYOUT_ID_Reg_Cores Then
                    If Not GetDgCoreWorkOrder(workOrderNo, cwoObj) Then Return False
                    Return ConvertCogentWoToWoDgCore(cwoObj, workOrder)
                Else
                    If Not GetToroidWorkOrder(workOrderNo, cwoObj) Then Return False
                    Return ConvertCogentWoToWoToroid(cwoObj, workOrder)
                End If
            End Function
            Private Shared Function GetTestType(workOrder As String, ByRef type As String) As Boolean
                Dim cwoObj As CogentWorkOrderComplex = Nothing
                Dim sqlGET As String = "SELECT" & " WO.BASE_ID AS WorkOrder," &
                    " WO.UDF_LAYOUT_ID As UDF_LAYOUT_ID" &
                    " FROM WORK_ORDER As WO" &
                    " WHERE WO.BASE_ID ='" & workOrder & "'"


                Dim datenListe As New List(Of CogentWorkOrderComplex)()
                Try
                    Dim tab As DataTable = ExecuteDataTable(sqlGET)


                    For j As Integer = 0 To tab.Rows.Count - 1
                        Dim data As New CogentWorkOrderComplex()
                        For i As Integer = 0 To tab.Rows(j).ItemArray.Length - 1

                            Dim myType As Type = GetType(CogentWorkOrderComplex)
                            Dim myPropInfo As PropertyInfo = myType.GetProperty(tab.Columns(i).ColumnName)
                            If tab.Rows(j).ItemArray(i).[GetType]() <> GetType(System.DBNull) Then
                                myPropInfo.SetValue(data, tab.Rows(j).ItemArray(i), Nothing)
                            End If
                        Next
                        datenListe.Add(data)
                    Next
                    If datenListe.Count < 1 Then
                        Return False
                    End If
                Catch e As Exception
                    Throw e
                    Return False
                End Try
                type = datenListe(0).UDF_LAYOUT_ID
                Return True
            End Function

            Public Shared Function RequestResults(ByVal workOrder As WorkOrder,
                                                  ByRef results As List(Of Result)) As Boolean
                results = New List(Of Result)

                If workOrder.Sample.SampleType = Sample.SampleTypeT.Toroid Then
                    If Not RequestToroidResults(workOrder, results) Then Return False
                    If results.Count > 0 Then Return True
                Else

                    If Not RequestDgCoreResults(workOrder, results) Then Return False
                    If results.Count > 0 Then Return True
                End If

                Return False
            End Function

            Public Shared Function InsertResult(ByVal result As Result, ByVal workOrder As WorkOrder) As Boolean
                Try
                    If workOrder.Sample.SampleType = Sample.SampleTypeT.DgCore Then
                        Return InsertDgCoreResult(result, workOrder)
                    Else
                        Return InsertToroidResult(result, workOrder)
                    End If
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".InsertResult(): ", Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try
            End Function
#End Region

#Region "Request DgCore work order"
            Private Shared Function GetDgCoreWorkOrder(workOrder As String, ByRef daten As CogentWorkOrderComplex) As Boolean
                Dim sqlGET As String = "SELECT" & " WO.BASE_ID AS WorkOrder," &
                    " WO.USER_1 As USER_1 ," &
                    " WO.USER_2 As USER_2 ," &
                    " WO.USER_3 As USER_3 ," &
                    " WO.USER_4 As USER_4 ," &
                    " WO.USER_5 As USER_5 ," &
                    " WO.USER_6 As USER_6 ," &
                    " WO.USER_7 As USER_7 ," &
                    " WO.USER_8 As USER_8 ," &
                    " WO.USER_9 As USER_9 ," &
                    " WO.USER_10 As USER_10 ," &
                    " WO.UDF_LAYOUT_ID As UDF_LAYOUT_ID ," &
                    " CPI_SOFT_LINK.DEMAND_BASE_ID As COa," &
                    " CPI_SOFT_LINK.DEMAND_SEQ_NO AS COb," &
                    " WO.DESIRED_QTY AS Qty," &
                    " CUSTOMER.ID AS CustID," &
                    " CUSTOMER.NAME AS CustName," &
                    " CUSTOMER_ORDER.CUSTOMER_PO_REF AS PO," &
                    " PART.DESCRIPTION AS Part_Description," &
                    " PART.ID AS PartID," &
                    " WO.PRODUCT_CODE AS ProductCode," &
                    " WO.DESIRED_WANT_DATE AS WantDate," &
                    " WO.DESIRED_QTY AS Qty2," &
                    " 1 AS Rng1," &
                    " WO.DESIRED_QTY AS Rng2," &
                    " WO.COMMODITY_CODE AS Grade," &
                    " 7650 AS MatDensity," &
                    " Wind.USER_4 AS StackFactor," &
                    " WO.USER_1 AS MatWidth," &
                    " Wind.USER_3 AS ID," &
                    " PART.WEIGHT AS Wght," &
                    " Wind.USER_2 AS Build," &
                    " WO.USER_4 AS WWidth," &
                    " WO.USER_5 AS WLength," &
                    " Test.USER_1 AS TestSetting," &
                    " Test.USER_2 AS TestVoltage," &
                    " Test.USER_3 AS FluxDensity," &
                    " Test.USER_6 AS MaxGreen," &
                    " Test.USER_7 AS MaxYellow," &
                    " Test.USER_8 AS MaxRed," &
                    " Test.USER_10 AS SampleRate," &
                    " Test.USER_5 AS MaxAmps," &
                    " Test.USER_4 AS Mult," &
                    " 'IN' AS Imp," &
                    " Wind.USER_7 AS NoGaps," &
                    " Wind.USER_8 AS Overlap," &
                    " WO.USER_3 AS GapLocation," &
                    " Wind.USER_5 AS FirstSheet," &
                    " Wind.USER_10 AS LastSheet," &
                    " '   .   ' AS [End]," &
                    " Form.USER_1 AS Blanc1," &
                    " Form.USER_2 AS Blanc2" &
                    " FROM WORK_ORDER As WO " &
                    " LEFT JOIN CPI_SOFT_LINK ON WO.BASE_ID = CPI_SOFT_LINK.SUPPLY_BASE_ID" &
                    " LEFT JOIN CUSTOMER_ORDER ON CPI_SOFT_LINK.DEMAND_BASE_ID = CUSTOMER_ORDER.ID" &
                    " LEFT JOIN CUSTOMER ON CUSTOMER_ORDER.CUSTOMER_ID = CUSTOMER.ID" &
                    " INNER JOIN PART ON WO.PART_ID = PART.ID" &
                    " LEFT JOIN OPERATION AS Wind ON WO.TYPE = Wind.WORKORDER_TYPE AND WO.BASE_ID = Wind.WORKORDER_BASE_ID AND Wind.SEQUENCE_NO = (SELECT MIN(SEQUENCE_NO) FROM OPERATION WHERE WORKORDER_TYPE = WO.TYPE AND WORKORDER_BASE_ID = WO.BASE_ID)" &
                    " LEFT JOIN OPERATION AS Test ON WO.TYPE = Test.WORKORDER_TYPE AND WO.BASE_ID = Test.WORKORDER_BASE_ID AND Test.RESOURCE_ID Like 'TEST%'" &
                    " LEFT JOIN OPERATION AS Form ON WO.TYPE = Form.WORKORDER_TYPE AND WO.BASE_ID = Form.WORKORDER_BASE_ID AND Form.RESOURCE_ID Like 'FORM%'" &
                    " WHERE WO.BASE_ID ='" & workOrder & "'"

                Dim datenListe As New List(Of CogentWorkOrderComplex)()
                Try
                    Dim tab As DataTable = ExecuteDataTable(sqlGET)


                    For j As Integer = 0 To tab.Rows.Count - 1
                        Dim data As New CogentWorkOrderComplex()
                        For i As Integer = 0 To tab.Rows(j).ItemArray.Length - 1

                            Dim myType As Type = GetType(CogentWorkOrderComplex)
                            Dim myPropInfo As PropertyInfo = myType.GetProperty(tab.Columns(i).ColumnName)
                            If tab.Rows(j).ItemArray(i).[GetType]() <> GetType(System.DBNull) Then
                                myPropInfo.SetValue(data, tab.Rows(j).ItemArray(i), Nothing)
                            End If
                        Next
                        datenListe.Add(data)
                    Next
                    If datenListe.Count < 1 Then
                        Return False
                    End If
                Catch e As Exception
                    Throw e
                    Return False
                End Try
                daten = datenListe(0)
                Return True
            End Function

            Private Shared Function ConvertCogentWoToWoDgCore(ByVal cogentWorkOrder As CogentWorkOrderComplex,
                                                              ByRef workOrder As WorkOrder) As Boolean
                Try
                    Dim dgCore As WorkOrderDgCore = New WorkOrderDgCore

                    With cogentWorkOrder
                        dgCore.CoreCode = .ProductCode.Remove(0, 2)
                        dgCore.CPI_CatalogueNo = .PartID
                        dgCore.CustomerName = .CustName
                        dgCore.CustomerID = .CustID
                        dgCore.Grade = .Grade
                        dgCore.PartDescription = .Part_Description
                        dgCore.PurchaseOrderNo = .PO
                        dgCore.LossLimit_MaxGreen = Val(.MaxGreen)
                        dgCore.LossLimit_MaxRed = Val(.MaxRed)
                        dgCore.LossLimit_MaxYellow = Val(.MaxYellow)
                        dgCore.NominalInduction = Val(.FluxDensity) / 10
                        dgCore.NominalV_T = Val(.TestVoltage)
                        dgCore.NominalWeight = .Wght
                        dgCore.OrderQuantity = CUInt(.Qty)
                        dgCore.WorkOrderNo = .WorkOrder
                        dgCore.Frequency = Core.Instance.CurrentSettings.FrequencyDefault
                        dgCore.TestSetting = .TestSetting

                        Dim newSample As Sample = Nothing
                        newSample.density = Core.Instance.CurrentSettings.DensityDefault '.MatDensity
                        newSample.weight = 0
                        newSample.MaterialWidth = Val(.USER_1)
                        newSample.SampleType = Sample.SampleTypeT.DgCore
                        newSample.InnerDiameter = (2 * Val(.WLength) + 2 * Val(.WWidth)) / System.Math.PI
                        newSample.OuterDiameter = newSample.InnerDiameter + 2 * Val(.Build)
                        newSample.StackingFactor = Val(.StackFactor.Remove(0, 1)) / 1000
                        dgCore.Sample = newSample
                    End With

                    workOrder = dgCore
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".ConvertCogentWoToWoDgCore() ", Logger.Logger.eStatus.Exception, ex)
                    workOrder = New WorkOrderDgCore
                    Return False
                End Try
                Return True
            End Function
#End Region

#Region "Request Toroid work order"
            Private Shared Function GetToroidWorkOrder(workOrder As String, ByRef daten As CogentWorkOrderComplex) As Boolean
                Dim sqlGET As String = "SELECT" & " WO.BASE_ID AS WorkOrder," &
                    " WO.USER_1 As USER_1 ," &
                    " WO.USER_2 As USER_2 ," &
                    " WO.USER_3 As USER_3 ," &
                    " WO.USER_4 As USER_4 ," &
                    " WO.USER_5 As USER_5 ," &
                    " WO.USER_6 As USER_6 ," &
                    " WO.USER_7 As USER_7 ," &
                    " WO.USER_8 As USER_8 ," &
                    " WO.USER_9 As USER_9 ," &
                    " WO.USER_10 As USER_10 ," &
                    " WO.UDF_LAYOUT_ID As UDF_LAYOUT_ID ," &
                    " CPI_SOFT_LINK.DEMAND_BASE_ID As COa," &
                    " CPI_SOFT_LINK.DEMAND_SEQ_NO AS COb," &
                    " WO.DESIRED_QTY AS Qty," &
                    " CUSTOMER.ID AS CustID," &
                    " CUSTOMER.NAME AS CustName," &
                    " CUSTOMER_ORDER.CUSTOMER_PO_REF AS PO," &
                    " PART.DESCRIPTION AS Part_Description," &
                    " PART.ID AS PartID," &
                    " WO.PRODUCT_CODE AS ProductCode," &
                    " WO.DESIRED_WANT_DATE AS WantDate," &
                    " WO.DESIRED_QTY AS Qty2," &
                    " 1 AS Rng1," &
                    " WO.DESIRED_QTY AS Rng2," &
                    " WO.COMMODITY_CODE AS Grade," &
                    " 7650 AS MatDensity," &
                    " Wind.USER_4 AS StackFactor," &
                    " WO.USER_1 AS MatWidth," &
                    " Wind.USER_3 AS ID," &
                    " PART.WEIGHT AS Wght," &
                    " Wind.USER_2 AS Build," &
                    " WO.USER_4 AS WWidth," &
                    " WO.USER_5 AS WLength," &
                    " Test.USER_2 AS NoTurns," &
                    " Test.USER_3 AS TestVoltage," &
                    " Test.USER_4 AS MaxAmps," &
                    " Test.USER_6 AS FluxDensity," &
                    " Test.USER_7 AS TestSetting," &
                    " 'IN' AS Imp," &
                    " Wind.USER_7 AS NoGaps," &
                    " Wind.USER_8 AS Overlap," &
                    " WO.USER_3 AS GapLocation," &
                    " Wind.USER_5 AS FirstSheet," &
                    " Wind.USER_10 AS LastSheet," &
                    " '   .   ' AS [End]," &
                    " Form.USER_1 AS Blanc1," &
                    " Form.USER_2 AS Blanc2" &
                    " FROM WORK_ORDER As WO " &
                    " LEFT JOIN CPI_SOFT_LINK ON WO.BASE_ID = CPI_SOFT_LINK.SUPPLY_BASE_ID" &
                    " LEFT JOIN CUSTOMER_ORDER ON CPI_SOFT_LINK.DEMAND_BASE_ID = CUSTOMER_ORDER.ID" &
                    " LEFT JOIN CUSTOMER ON CUSTOMER_ORDER.CUSTOMER_ID = CUSTOMER.ID" &
                    " INNER JOIN PART ON WO.PART_ID = PART.ID" &
                    " LEFT JOIN OPERATION AS Wind ON WO.TYPE = Wind.WORKORDER_TYPE AND WO.BASE_ID = Wind.WORKORDER_BASE_ID AND Wind.SEQUENCE_NO = (SELECT MIN(SEQUENCE_NO) FROM OPERATION WHERE WORKORDER_TYPE = WO.TYPE AND WORKORDER_BASE_ID = WO.BASE_ID)" &
                    " LEFT JOIN OPERATION AS Test ON WO.TYPE = Test.WORKORDER_TYPE AND WO.BASE_ID = Test.WORKORDER_BASE_ID AND Test.RESOURCE_ID Like '%TEST'" &
                    " LEFT JOIN OPERATION AS Form ON WO.TYPE = Form.WORKORDER_TYPE AND WO.BASE_ID = Form.WORKORDER_BASE_ID AND Form.RESOURCE_ID Like 'FORM%'" &
                    " WHERE WO.BASE_ID ='" & workOrder & "'"

                Dim datenListe As New List(Of CogentWorkOrderComplex)()
                Try
                    Dim tab As DataTable = ExecuteDataTable(sqlGET)


                    For j As Integer = 0 To tab.Rows.Count - 1
                        Dim data As New CogentWorkOrderComplex()
                        For i As Integer = 0 To tab.Rows(j).ItemArray.Length - 1

                            Dim myType As Type = GetType(CogentWorkOrderComplex)
                            Dim myPropInfo As PropertyInfo = myType.GetProperty(tab.Columns(i).ColumnName)
                            If tab.Rows(j).ItemArray(i).[GetType]() <> GetType(System.DBNull) Then
                                myPropInfo.SetValue(data, tab.Rows(j).ItemArray(i), Nothing)
                            End If
                        Next
                        datenListe.Add(data)
                    Next
                    If datenListe.Count < 1 Then
                        Return False
                    End If
                Catch e As Exception
                    Throw e
                    Return False
                End Try
                daten = datenListe(0)
                Return True
            End Function

            Private Shared Function ConvertCogentWoToWoToroid(ByVal cogentWorkOrder As CogentWorkOrderComplex,
                                                              ByRef workOrder As WorkOrder) As Boolean
                Try
                    Dim toroid As New WorkOrderToroid
                    With cogentWorkOrder
                        toroid.CoreCode = .ProductCode.Remove(0, 2)
                        toroid.CPI_CatalogueNo = .PartID
                        toroid.CustomerName = .CustName
                        toroid.CustomerID = .CustID
                        toroid.Grade = .Grade
                        toroid.PartDescription = .Part_Description
                        toroid.PurchaseOrderNo = .PO
                        toroid.NominalInduction = Val(.FluxDensity)
                        toroid.NominalV_T = Val(.TestVoltage)
                        toroid.NominalWeight = .Wght
                        toroid.OrderQuantity = CUInt(.Qty)
                        toroid.WorkOrderNo = .WorkOrder
                        toroid.Frequency = Core.Instance.CurrentSettings.FrequencyDefault
                        toroid.MaxAmps = Val(.MaxAmps)
                        toroid.OperatorMaxAmps = 0
                        toroid.NumberOfTurns = CInt(Val(.NoTurns))
                        toroid.OperatorNumberOfTurns = 0
                        toroid.TestSetting = .TestSetting

                        Dim newSample As Sample = Nothing
                        newSample.density = Core.Instance.CurrentSettings.DensityDefault
                        newSample.weight = 0
                        newSample.MaterialWidth = Val(.USER_1)
                        newSample.SampleType = Sample.SampleTypeT.Toroid
                        newSample.InnerDiameter = Val(.USER_3)
                        newSample.OuterDiameter = Val(.USER_4)
                        newSample.StackingFactor = Core.Instance.CurrentSettings.ToroidDefaultStackingFactor

                        toroid.Sample = newSample
                    End With

                    workOrder = toroid
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".ConvertCogentWoToWoToroid() ", Logger.Logger.eStatus.Exception, ex)
                    workOrder = New WorkOrderToroid
                    Return False
                End Try
                Return True
            End Function
#End Region

#Region "Request DgCore results"
            Private Shared Function RequestDgCoreResults(ByVal workOrder As WorkOrder,
                                              ByRef results As List(Of Result)) As Boolean
                Dim newResults As New List(Of CogentDgCoreResult)
                Dim newHeader As New CogentDgCoreHeader

                Try
                    If Not RequestDgCoreResultsSQL(workOrder.WorkOrderNo, newResults) Then Return False
                    If Not RequestDgCoreHeaderSQL(workOrder.WorkOrderNo, newHeader) Then Return False

                    For Each element In newResults
                        Dim newResult As New Result
                        With newResult
                            .AmpTurns = element.ATrms
                            .Date = element.DATE_TESTED

                            If element.SUB_LINE.Trim.Length > 0 Then
                                For Each s In element.SUB_LINE.Trim
                                    If s = "-" Then Continue For
                                    If IsNumeric(s) Then : Integer.TryParse(s, .Retest)
                                    Else : .DuplicateTest = s
                                    End If
                                Next
                            Else
                                .DuplicateTest = ""
                                .Retest = 0
                            End If
                            .Serial = element.LINE.ToString
                            .User = element.USER_ID
                            .Watts = element.WATTS
                            .Weight = element.WEIGHT

                            Dim dgCoreWo As WorkOrderDgCore = TryCast(workOrder, WorkOrderDgCore)
                            If dgCoreWo Is Nothing Then Return False

                            .Grade = Math.GetDgCoreGrade(dgCoreWo.LossLimit_MaxGreen,
                                                   dgCoreWo.LossLimit_MaxYellow,
                                                   dgCoreWo.LossLimit_MaxRed, .Watts)
                            .PercentageGreenLossLevel = CDbl(FormatNumber(Math.PercentageGreenLossLevel(element.WATTS, dgCoreWo.LossLimit_MaxGreen), 2))
                            '.PercentageActualVT = Math.PercentageVoltsTurn(element.coilsystem.n2, element.uMax)
                            .MpgDevice = element.MpgDevice
                        End With
                        results.Add(newResult)
                    Next
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".RequestDgCoreResults() workOrderNo: " + workOrder.WorkOrderNo, Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try
                Return True
            End Function
            Private Shared Function RequestDgCoreResultsSQL(WORK_ORDER_ID As String, ByRef datenListe As List(Of CogentDgCoreResult)) As [Boolean]
                Dim sqlGET As String = "SELECT" & " * " & " from CPI_WO_TEST_LINE " & "WHERE CPI_WO_TEST_LINE.WORK_ORDER_ID ='" & WORK_ORDER_ID & "'"
                Try
                    Dim tab As DataTable = ExecuteDataTable(sqlGET)
                    tab.TableName = "CPI_WO_TEST_LINE"

                    If tab.Rows.Count = 0 Then Return False

                    For j As Integer = 0 To tab.Rows.Count - 1
                        Dim data As New CogentDgCoreResult()
                        For i As Integer = 0 To tab.Rows(0).ItemArray.Length - 1

                            Dim myType As Type = GetType(CogentDgCoreResult)
                            Dim myPropInfo As PropertyInfo = myType.GetProperty(tab.Columns(i).ColumnName)
                            If tab.Rows(j).ItemArray(i).[GetType]() <> GetType(System.DBNull) Then
                                myPropInfo.SetValue(data, tab.Rows(j).ItemArray(i), Nothing)
                            End If
                        Next
                        datenListe.Add(data)
                    Next
                    If datenListe.Count < 1 Then
                        Return False
                    End If

                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".RequestDgCoreResultsSQL() workOrderNo: " + WORK_ORDER_ID, Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try

                Return True
            End Function
            Private Shared Function RequestDgCoreResultSQL(ByVal WORK_ORDER_ID As String, ByVal LINE As Decimal, ByVal SUB_LINE As String, ByRef result As CogentDgCoreResult) As Boolean
                Dim sqlGET As String = "SELECT * FROM CPI_WO_TEST_LINE " +
                                       "WHERE WORK_ORDER_ID='" + WORK_ORDER_ID + "' AND LINE=" + LINE.ToString + " AND SUB_LINE='" + SUB_LINE + "'"
                Try
                    Dim tab As DataTable = ExecuteDataTable(sqlGET)
                    tab.TableName = "CPI_WO_TEST_LINE"

                    If tab.Rows.Count = 0 Then Return False

                    For j As Integer = 0 To tab.Rows.Count - 1
                        Dim data As New CogentDgCoreResult()
                        For i As Integer = 0 To tab.Rows(0).ItemArray.Length - 1

                            Dim myType As Type = GetType(CogentDgCoreResult)
                            Dim myPropInfo As PropertyInfo = myType.GetProperty(tab.Columns(i).ColumnName)
                            If tab.Rows(j).ItemArray(i).[GetType]() <> GetType(System.DBNull) Then
                                myPropInfo.SetValue(data, tab.Rows(j).ItemArray(i), Nothing)
                            End If
                        Next
                        result = data
                        Return True
                    Next
                    Return False
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".RequestDgCoreResultSQL() workOrderNo: " + WORK_ORDER_ID, Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try
            End Function
            Private Shared Function RequestDgCoreHeaderSQL(workOrder As String, ByRef data As CogentDgCoreHeader) As [Boolean]
                Dim sqlGET As String = "SELECT" & " * " & " from CPI_WO_TEST " & "WHERE CPI_WO_TEST.WORK_ORDER_ID ='" & workOrder & "'"
                Try
                    Dim tab As DataTable = ExecuteDataTable(sqlGET)
                    tab.TableName = "CPI_WO_TEST"

                    If tab.Rows.Count = 0 Then Return False

                    For i As Integer = 0 To tab.Rows(0).ItemArray.Length - 1
                        Dim myType As Type = GetType(CogentDgCoreHeader)
                        Dim myPropInfo As PropertyInfo = myType.GetProperty(tab.Columns(i).ColumnName)
                        If tab.Rows(0).ItemArray(i).[GetType]() <> GetType(System.DBNull) Then
                            myPropInfo.SetValue(data, tab.Rows(0).ItemArray(i), Nothing)
                        End If
                    Next
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".RequestDgCoreHeaderSQL() workOrderNo: " + workOrder, Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try
                Return True
            End Function
#End Region

#Region "Request Toroid results"
            Private Shared Function RequestToroidResults(ByVal workOrder As WorkOrder,
                                                          ByRef results As List(Of Result)) As Boolean
                Dim newResults As New List(Of CogentToroidResult)
                Dim newHeader As New CogentToroidHeader

                Try
                    If Not CogentDB.RequestToroidResultsSQL(workOrder.WorkOrderNo, newResults) Then Return False
                    If Not CogentDB.RequestToroidHeaderSQL(workOrder.WorkOrderNo, newHeader) Then Return False

                    For Each element In newResults
                        Dim newResult As New Result
                        With newResult
                            .Amps = element.ATrms
                            .Date = newHeader.DATE_TEST
                            [Enum].TryParse(element.Quality, .Grade)
                            .Retest = element.RetestNo
                            .Serial = element.SerialNo.ToString
                            .User = element.Operator
                            .Watts = element.Watts
                            .DuplicateTest = element.Duplicate.Trim
                            .Weight = element.Weight
                            .NumberOfTurns = If(element.NumberOfTurns = 0, 1, element.NumberOfTurns)
                            .Volts = element.VoltsTurn
                            .MpgDevice = element.MpgDevice

                            Dim toroidWo As WorkOrderToroid = TryCast(workOrder, WorkOrderToroid)
                            If toroidWo Is Nothing Then Return False
                            Dim currentMaxAmps As Double = toroidWo.MaxAmps / (.NumberOfTurns / toroidWo.NumberOfTurns)
                            .Grade = Math.GetToroidGrade(currentMaxAmps, .Amps)
                            .PercentageGreenLossLevel = CDbl(FormatNumber(Math.PercentageGreenLossLevel(element.ATrms, currentMaxAmps), 2))
                        End With
                        results.Add(newResult)
                    Next
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".RequestToroidResults() workOrderNo: " + workOrder.WorkOrderNo, Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try
                Return True
            End Function
            Private Shared Function RequestToroidResultsSQL(WORK_ORDER_ID As String, ByRef datenListe As List(Of CogentToroidResult)) As [Boolean]
                Dim sqlGET As String = "SELECT" & " * " & " from CPI_TOROID_TEST_LINE " & "WHERE CPI_TOROID_TEST_LINE.WORK_ORDER_ID ='" & WORK_ORDER_ID & "'"
                Try
                    Dim tab As DataTable = ExecuteDataTable(sqlGET)
                    tab.TableName = "CPI_TOROID_TEST_LINE"

                    If tab.Rows.Count = 0 Then Return False

                    For j As Integer = 0 To tab.Rows.Count - 1
                        Dim data As New CogentToroidResult()
                        For i As Integer = 0 To tab.Rows(0).ItemArray.Length - 1

                            Dim myType As Type = GetType(CogentToroidResult)
                            Dim myPropInfo As PropertyInfo = myType.GetProperty(tab.Columns(i).ColumnName)
                            If tab.Rows(j).ItemArray(i).[GetType]() <> GetType(System.DBNull) Then
                                myPropInfo.SetValue(data, tab.Rows(j).ItemArray(i), Nothing)
                            End If
                        Next
                        datenListe.Add(data)
                    Next
                    If datenListe.Count < 1 Then
                        Return False
                    End If

                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".RequestToroidResultsSQL() workOrderNo: " + WORK_ORDER_ID, Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try

                Return True
            End Function
            Private Shared Function RequestToroidResultSQL(ByVal WORK_ORDER_ID As String,
                                                            ByVal SerialNo As Integer,
                                                            ByVal RetestNo As Integer,
                                                            ByVal Duplicate As String, ByRef result As CogentToroidResult) As [Boolean]
                Dim sqlGET As String = "SELECT * from CPI_TOROID_TEST_LINE " +
                                       "WHERE WORK_ORDER_ID='" + WORK_ORDER_ID + "' AND SerialNo='" + SerialNo.ToString + "' AND RetestNo ='" + RetestNo.ToString + "' AND Duplicate = '" + Duplicate + "'"
                Try
                    Dim tab As DataTable = ExecuteDataTable(sqlGET)
                    tab.TableName = "CPI_TOROID_TEST_LINE"

                    If tab.Rows.Count = 0 Then Return False

                    For j As Integer = 0 To tab.Rows.Count - 1
                        Dim data As New CogentToroidResult()
                        For i As Integer = 0 To tab.Rows(0).ItemArray.Length - 1

                            Dim myType As Type = GetType(CogentToroidResult)
                            Dim myPropInfo As PropertyInfo = myType.GetProperty(tab.Columns(i).ColumnName)
                            If tab.Rows(j).ItemArray(i).[GetType]() <> GetType(System.DBNull) Then
                                myPropInfo.SetValue(data, tab.Rows(j).ItemArray(i), Nothing)
                            End If
                        Next
                        result = data
                        Return True
                    Next
                    Return False
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".RequestToroidResultSQL() workOrderNo: " + WORK_ORDER_ID, Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try

                Return True
            End Function
            Private Shared Function RequestToroidHeaderSQL(WORK_ORDER_ID As String, ByRef data As CogentToroidHeader) As [Boolean]
                Dim sqlGET As String = "SELECT" & " * " & " from CPI_TOROID_TEST " & "WHERE CPI_TOROID_TEST.WORK_ORDER_ID ='" & WORK_ORDER_ID & "'"
                Try
                    Dim tab As DataTable = ExecuteDataTable(sqlGET)
                    tab.TableName = "CPI_TOROID_TEST"

                    If tab.Rows.Count = 0 Then Return False

                    For i As Integer = 0 To tab.Rows(0).ItemArray.Length - 1

                        Dim myType As Type = GetType(CogentToroidHeader)
                        Dim myPropInfo As PropertyInfo = myType.GetProperty(tab.Columns(i).ColumnName)
                        If tab.Rows(0).ItemArray(i).[GetType]() <> GetType(System.DBNull) Then
                            myPropInfo.SetValue(data, tab.Rows(0).ItemArray(i), Nothing)
                        End If
                    Next

                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".RequestToroidHeaderSQL() workOrderNo: " + WORK_ORDER_ID, Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try

                Return True
            End Function
#End Region

#Region "Insert DgCore results"
            Private Shared Function InsertDgCoreResult(ByVal result As Result, ByVal workOrder As WorkOrder) As Boolean
                Dim newResult As CogentDgCoreResult = Nothing

                'Get cogent rectangular core object for DB
                If Not ConvertResultToCogentDgCoreResult(workOrder.WorkOrderNo, result, newResult) Then Return False

                'If: no result header exists -> create a new one and insert core object
                'else: check if core object exists -> insert or update
                If Not RequestDgCoreHeaderSQL(workOrder.WorkOrderNo, New CogentDgCoreHeader) Then
                    If Not InsertDgCoreHeader(workOrder) Then Return False
                    Return InsertDgCoreResultSQL(newResult)
                Else
                    If Not RequestDgCoreResultSQL(newResult.WORK_ORDER_ID, newResult.LINE, newResult.SUB_LINE, New CogentDgCoreResult) Then
                        Return InsertDgCoreResultSQL(newResult)
                    Else
                        Return UpdateDgCoreResultSQL(newResult)
                    End If
                End If
            End Function
            Private Shared Function ConvertResultToCogentDgCoreResult(ByVal workOrderNo As String,
                                                                        ByVal result As Result,
                                                                        ByRef cogentResult As CogentDgCoreResult) As Boolean
                Try
                    cogentResult = New CogentDgCoreResult
                    With cogentResult
                        .ATrms = CDec(result.AmpTurns)
                        .LINE = CInt(result.Serial)
                        .RESULT = DBNull.Value.ToString
                        .SUB_LINE = result.Test
                        .WATTS = CDec(result.Watts)
                        .WEIGHT = CDec(result.Weight)
                        .WORK_ORDER_ID = workOrderNo
                        .USER_ID = result.User
                        .DATE_TESTED = result.Date
                        .MpgDevice = result.MpgDevice
                    End With
                    Return True
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".ConvertResultToCogentDgCoreResult(): ", Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try
            End Function

            Private Shared Function InsertDgCoreHeader(ByVal workOrder As WorkOrder) As Boolean
                Try
                    Dim newHeader As New CogentDgCoreHeader
                    With newHeader
                        .CATALOGUE_NO = workOrder.CPI_CatalogueNo
                        .CUSTOMER_ID = workOrder.CustomerID
                        .CUSTOMER_PART_NO = workOrder.PartDescription
                        .CUSTOMER_PO = workOrder.PurchaseOrderNo
                        .DATE_TEST = System.DateTime.Now
                        .DATETIME_TEST = System.DateTime.Now
                        .FLUX_DENSITY = CDec(workOrder.NominalInduction)
                        .GRADE = workOrder.Grade
                        .WORK_ORDER_ID = workOrder.WorkOrderNo
                    End With

                    Return InsertDgCoreHeaderSQL(newHeader)
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".InsertDgCoreHeader(): ", Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try
            End Function
            Private Shared Function InsertDgCoreHeaderSQL(data As CogentDgCoreHeader) As Boolean
                Try
                    Dim sqlIN As String = "INSERT INTO CPI_WO_TEST ([WORK_ORDER_ID] ,[CATALOGUE_NO] ,[CUSTOMER_ID] ,[CUSTOMER_PO] ,[CUSTOMER_PART_NO] ,[DATE_TEST] ,[DATETIME_TEST] ,[GRADE] ,[FLUX_DENSITY]) VALUES('" &
                    data.WORK_ORDER_ID & "', '" &
                    data.CATALOGUE_NO & "', '" &
                    data.CUSTOMER_ID & "' ,'" &
                    data.CUSTOMER_PO & "', '" &
                    data.CUSTOMER_PART_NO & "', '" &
                    data.DATE_TEST.ToString("MM-dd-yy").Replace("-", "/") & "', '" &
                    data.DATETIME_TEST.ToString("MM-dd-yy hh:mm:ss").Replace("-", "/") & "', '" &
                    data.GRADE & "', " &
                    data.FLUX_DENSITY.ToString(Globalization.CultureInfo.InvariantCulture) & ")"

                    Return ExecuteNoN(sqlIN)
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".InsertDgCoreHeaderSQL(): ", Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try
            End Function
            Private Shared Function UpdateDgCoreResultSQL(data As CogentDgCoreResult) As Boolean
                Try
                    Dim sqlIn As String =
                        "UPDATE CPI_WO_TEST_LINE " +
                        "SET [WEIGHT]=" + data.WEIGHT.ToString(Globalization.CultureInfo.InvariantCulture) + ", " +
                        "[WATTS]=" + data.WATTS.ToString(Globalization.CultureInfo.InvariantCulture) + ", " +
                        "[ATrms]=" + data.ATrms.ToString(Globalization.CultureInfo.InvariantCulture) + ", " +
                        "[RESULT]='" + data.RESULT + "', " +
                        "[USER_ID]='" + data.USER_ID + "', " +
                        "[DATE_TESTED]='" + data.DATE_TESTED.ToString("MM-dd-yy").Replace("-", "/") + "'," +
                        "[MpgDevice]='" + data.MpgDevice + "'" +
                        "WHERE [WORK_ORDER_ID]='" + data.WORK_ORDER_ID + "' AND [LINE]=" + data.LINE.ToString + " AND [SUB_LINE]='" + data.SUB_LINE + "'"

                    Return ExecuteNoN(sqlIn)
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".UpdateDgCoreResultSQL(): ", Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try
            End Function
            Private Shared Function InsertDgCoreResultSQL(data As CogentDgCoreResult) As [Boolean]
                Try
                    Dim sqlIN As String = "INSERT INTO CPI_WO_TEST_LINE ([WORK_ORDER_ID] ,[LINE] ,[SUB_LINE] ,[WEIGHT] ,[WATTS] ,[ATrms] ,[RESULT] ,[USER_ID] ,[DATE_TESTED], [MpgDevice]) VALUES('" &
                    data.WORK_ORDER_ID & "', " &
                    data.LINE.ToString & ", '" &
                    data.SUB_LINE & "' ," &
                    data.WEIGHT.ToString(Globalization.CultureInfo.InvariantCulture) & ", " &
                    data.WATTS.ToString(Globalization.CultureInfo.InvariantCulture) & ", " &
                    data.ATrms.ToString(Globalization.CultureInfo.InvariantCulture) & ", '" &
                    data.RESULT & "', '" &
                    data.USER_ID & "', '" &
                    data.DATE_TESTED.ToString("MM-dd-yy").Replace("-", "/") & "', '" &
                    data.MpgDevice & "')"

                    Return ExecuteNoN(sqlIN)
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".InsertDgCoreResultSQL(): ", Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try
            End Function
#End Region

#Region "Insert Toroid results"
            Private Shared Function InsertToroidResult(ByVal result As Result, ByVal workOrder As WorkOrder) As Boolean
                Dim newResult As CogentToroidResult = Nothing

                If Not ConvertResultToCogentToroidResult(workOrder.WorkOrderNo, result, newResult) Then Return False

                'If: no result header exists -> create a new one and insert core object
                'else: check if core object exists -> insert or update
                If Not RequestToroidHeaderSQL(workOrder.WorkOrderNo, New CogentToroidHeader) Then
                    If Not InsertToroidHeader(workOrder) Then Return False
                    Return InsertToroidResultSQL(newResult)
                Else
                    If Not RequestToroidResultSQL(newResult.WORK_ORDER_ID, newResult.SerialNo, newResult.RetestNo, newResult.Duplicate, New CogentToroidResult) Then
                        Return InsertToroidResultSQL(newResult)
                    Else
                        Return UpdateToroidResultSQL(newResult)
                    End If
                End If
            End Function
            Private Shared Function ConvertResultToCogentToroidResult(ByVal workOrderNo As String,
                                                                      ByVal result As Result,
                                                                      ByRef cogentResult As CogentToroidResult) As Boolean
                Try
                    cogentResult = New CogentToroidResult
                    With cogentResult
                        .ATrms = CDec(result.Amps)
                        .BHV = DBNull.Value.ToString
                        .Bpk = 0
                        .Duplicate = result.DuplicateTest
                        .Operator = result.User
                        .PhaseAngle = 0
                        .Quality = result.Grade.ToString
                        .RetestNo = result.Retest
                        .SerialNo = CInt(result.Serial)
                        .Setval = 0
                        .VAs = 0
                        .VoltsTurn = CDec(result.Volts)
                        .Watts = 0
                        .WORK_ORDER_ID = workOrderNo
                        .DATE_TESTED = result.Date
                        .Weight = CDec(result.Weight)
                        .NumberOfTurns = result.NumberOfTurns
                        .MpgDevice = result.MpgDevice
                    End With
                    Return True
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".ConvertResultToCogentToroidResult(): ", Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try
            End Function

            Private Shared Function InsertToroidHeader(ByVal workOrder As WorkOrder) As Boolean
                Try
                    Dim newHeader As New CogentToroidHeader
                    With newHeader
                        .CATALOGUE_NO = workOrder.CPI_CatalogueNo
                        .CUSTOMER_ID = workOrder.CustomerID
                        .CUSTOMER_PART_NO = workOrder.PartDescription
                        .DATE_TEST = System.DateTime.Now
                        .GRADE = workOrder.Grade
                        .WORK_ORDER_ID = workOrder.WorkOrderNo
                    End With

                    Return InsertToroidHeaderSQL(newHeader)
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".InsertToroidHeader(): ", Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try
            End Function
            Private Shared Function InsertToroidHeaderSQL(data As CogentToroidHeader) As Boolean
                Try
                    Dim sqlIN As String = "INSERT INTO CPI_TOROID_TEST ([WORK_ORDER_ID] ,[CATALOGUE_NO] ,[CUSTOMER_ID] ,[CUSTOMER_PART_NO] ,[DATE_TEST] ,[GRADE]) VALUES('" &
                    data.WORK_ORDER_ID & "', '" &
                    data.CATALOGUE_NO & "', '" &
                    data.CUSTOMER_ID & "' ,'" &
                    data.CUSTOMER_PART_NO & "', '" &
                    data.DATE_TEST.ToString("MM-dd-yy").Replace("-", "/") & "', '" &
                    data.GRADE & "')"

                    Return ExecuteNoN(sqlIN)
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".InsertToroidHeaderSQL(): ", Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try
            End Function
            Private Shared Function UpdateToroidResultSQL(data As CogentToroidResult) As Boolean
                Try
                    Dim sqlIn As String =
                        "UPDATE CPI_TOROID_TEST_LINE " +
                        "SET [Operator]='" + data.Operator + "', " +
                        "[BHV]='" + data.BHV + "', " +
                        "[Setval]=" + data.Setval.ToString(Globalization.CultureInfo.InvariantCulture) + ", " +
                        "[Bpk]=" + data.Bpk.ToString(Globalization.CultureInfo.InvariantCulture) + ", " +
                        "[VoltsTurn]=" + data.VoltsTurn.ToString(Globalization.CultureInfo.InvariantCulture) + ", " +
                        "[Watts]=" + data.Watts.ToString(Globalization.CultureInfo.InvariantCulture) + ", " +
                        "[VAs]=" + data.VAs.ToString(Globalization.CultureInfo.InvariantCulture) + ", " +
                        "[ATrms]=" + data.ATrms.ToString(Globalization.CultureInfo.InvariantCulture) + ", " +
                        "[PhaseAngle]=" + data.PhaseAngle.ToString(Globalization.CultureInfo.InvariantCulture) + ", " +
                        "[Quality]='" + data.Quality + "', " +
                        "[USER_ID]='" + data.Operator + "', " +
                        "[DATE_TESTED]='" + data.DATE_TESTED.ToString("MM-dd-yy").Replace("-", "/") + "' ," +
                        "[Weight]=" + data.Weight.ToString(Globalization.CultureInfo.InvariantCulture) + ", " +
                        "[NumberOfTurns]=" + data.NumberOfTurns.ToString(Globalization.CultureInfo.InvariantCulture) + ", " +
                        "[MpgDevice]='" + data.MpgDevice + "'" +
                        "WHERE [WORK_ORDER_ID]='" + data.WORK_ORDER_ID +
                        "' AND [SerialNo]=" + data.SerialNo.ToString +
                        " AND [RetestNo]=" + data.RetestNo.ToString +
                        " AND [Duplicate]='" + data.Duplicate + "'"

                    Return ExecuteNoN(sqlIn)
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".UpdateToroidResultSQL(): ", Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try
            End Function
            Private Shared Function InsertToroidResultSQL(data As CogentToroidResult) As [Boolean]
                Try
                    Dim sqlIN As String = "INSERT INTO CPI_TOROID_TEST_LINE ([WORK_ORDER_ID] ,[SerialNo] ,[RetestNo] ,[Duplicate] ,[Operator] ,[BHV] ,[Setval] ,[Bpk] ,[VoltsTurn] ,[Watts] ,[VAs] ,[ATrms] ,[PhaseAngle] ,[Quality] ,[USER_ID] ,[DATE_TESTED] ,[Weight] ,[NumberOfTurns], [MpgDevice]) VALUES('" &
                    data.WORK_ORDER_ID & "', " &
                    data.SerialNo.ToString & ", " &
                    data.RetestNo.ToString & ", '" &
                    data.Duplicate & "', '" &
                    data.Operator & "', '" &
                    data.BHV & "', " &
                    data.Setval.ToString(Globalization.CultureInfo.InvariantCulture) & ", " &
                    data.Bpk.ToString(Globalization.CultureInfo.InvariantCulture) & ", " &
                    data.VoltsTurn.ToString(Globalization.CultureInfo.InvariantCulture) & ", " &
                    data.Watts.ToString(Globalization.CultureInfo.InvariantCulture) & ", " &
                    data.VAs.ToString(Globalization.CultureInfo.InvariantCulture) & ", " &
                    data.ATrms.ToString(Globalization.CultureInfo.InvariantCulture) & ", " &
                    data.PhaseAngle.ToString(Globalization.CultureInfo.InvariantCulture) & ", '" &
                    data.Quality & "', '" &
                    data.Operator & "', '" &
                    data.DATE_TESTED.ToString("MM-dd-yy").Replace("-", "/") & "', " &
                    data.Weight.ToString(Globalization.CultureInfo.InvariantCulture) & ", " &
                    data.NumberOfTurns.ToString(Globalization.CultureInfo.InvariantCulture) & ", '" &
                    data.MpgDevice & "')"


                    Return ExecuteNoN(sqlIN)
                Catch ex As Exception
                    Logger.Logger.Instance.Log(GetType(CogentDB).ToString + ".InsertToroidResultSQL(): ", Logger.Logger.eStatus.Exception, ex)
                    Return False
                End Try
            End Function
#End Region
        End Class
    End Class
End Namespace
