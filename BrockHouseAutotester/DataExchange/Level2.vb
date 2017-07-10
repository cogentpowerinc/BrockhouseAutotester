Imports System.Threading.Tasks
Imports DataElements

Namespace DataExchange
    Public Class Level2
        Public Function RequestData(ByVal workOrderNo As String) As Boolean
            Dim workOrder As WorkOrder = Nothing
            If Not CogentDB.RequestWorkOrder(workOrderNo, workOrder) Then Return False
            Core.Instance.CurrentWorkOrder = workOrder

            Dim results As List(Of Result) = Nothing
            If Not CogentDB.RequestResults(workOrder, results) Then Return False

            Core.Instance.CurrentResults = results

            Return True
        End Function
        Private _requestDataTask As Task = Nothing

        Public Function SaveResult(ByVal workOrder As WorkOrder, ByVal newResult As Result) As Boolean
            Try
                If newResult.State = Result.StateT.Abandon Then Return False

                Return CogentDB.InsertResult(newResult, workOrder)
            Catch ex As Exception
                Logger.Logger.Instance.Log(GetType(Level2).ToString + ".SaveResult() workOrderNo: " + workOrder.WorkOrderNo, Logger.Logger.eStatus.Exception, ex)
                Return False
            End Try
            Return True
        End Function
    End Class
End Namespace
