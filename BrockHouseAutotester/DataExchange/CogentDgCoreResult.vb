Imports System.ComponentModel

Namespace DataExchange
    Partial Class Level2
        Private Class CogentDgCoreResult
            Private _WORK_ORDER_ID As System.String
            <Browsable(True)> _
            Public Property WORK_ORDER_ID() As System.String
                Get
                    Return _WORK_ORDER_ID
                End Get
                Set(value As System.String)
                    _WORK_ORDER_ID = value
                End Set
            End Property

            Private _LINE As System.Int32
            <Browsable(True)> _
            Public Property LINE() As System.Int32
                Get
                    Return _LINE
                End Get
                Set(value As System.Int32)
                    _LINE = value
                End Set
            End Property

            Private _SUB_LINE As System.String
            <Browsable(True)> _
            Public Property SUB_LINE() As System.String
                Get
                    Return _SUB_LINE
                End Get
                Set(value As System.String)
                    _SUB_LINE = value
                End Set
            End Property

            Private _WEIGHT As System.Decimal
            <Browsable(True)> _
            Public Property WEIGHT() As System.Decimal
                Get
                    Return _WEIGHT
                End Get
                Set(value As System.Decimal)
                    _WEIGHT = value
                End Set
            End Property

            Private _WATTS As System.Decimal
            <Browsable(True)> _
            Public Property WATTS() As System.Decimal
                Get
                    Return _WATTS
                End Get
                Set(value As System.Decimal)
                    _WATTS = value
                End Set
            End Property

            Private _ATrms As System.Decimal
            <Browsable(True)> _
            Public Property ATrms() As System.Decimal
                Get
                    Return _ATrms
                End Get
                Set(value As System.Decimal)
                    _ATrms = value
                End Set
            End Property

            Private _RESULT As System.String
            <Browsable(True)> _
            Public Property RESULT() As System.String
                Get
                    Return _RESULT
                End Get
                Set(value As System.String)
                    _RESULT = value
                End Set
            End Property

            Public Property USER_ID As String

            Public Property DATE_TESTED As DateTime

            Public Property MpgDevice As String
        End Class
    End Class
End Namespace
