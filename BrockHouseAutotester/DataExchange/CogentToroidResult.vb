Imports System.ComponentModel

Namespace DataExchange
    Partial Class Level2
        Private Class CogentToroidResult
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

            Private _SerialNo As System.Int32
            <Browsable(True)> _
            Public Property SerialNo() As System.Int32
                Get
                    Return _SerialNo
                End Get
                Set(value As System.Int32)
                    _SerialNo = value
                End Set
            End Property

            Private _RetestNo As System.Int32
            <Browsable(True)> _
            Public Property RetestNo() As System.Int32
                Get
                    Return _RetestNo
                End Get
                Set(value As System.Int32)
                    _RetestNo = value
                End Set
            End Property

            Private _Duplicate As System.String
            <Browsable(True)> _
            Public Property Duplicate() As System.String
                Get
                    Return _Duplicate
                End Get
                Set(value As System.String)
                    _Duplicate = value
                End Set
            End Property

            Private _Operator As System.String
            <Browsable(True)> _
            Public Property [Operator]() As System.String
                Get
                    Return _Operator
                End Get
                Set(value As System.String)
                    _Operator = value
                End Set
            End Property

            Private _BHV As System.String
            <Browsable(True)> _
            Public Property BHV() As System.String
                Get
                    Return _BHV
                End Get
                Set(value As System.String)
                    _BHV = value
                End Set
            End Property

            Private _Setval As System.Decimal
            <Browsable(True)> _
            Public Property Setval() As System.Decimal
                Get
                    Return _Setval
                End Get
                Set(value As System.Decimal)
                    _Setval = value
                End Set
            End Property

            Private _Bpk As System.Decimal
            <Browsable(True)> _
            Public Property Bpk() As System.Decimal
                Get
                    Return _Bpk
                End Get
                Set(value As System.Decimal)
                    _Bpk = value
                End Set
            End Property

            Private _Volts As System.Decimal
            <Browsable(True)> _
            Public Property VoltsTurn() As System.Decimal
                Get
                    Return _Volts
                End Get
                Set(value As System.Decimal)
                    _Volts = value
                End Set
            End Property

            Private _Watts As System.Decimal
            <Browsable(True)> _
            Public Property Watts() As System.Decimal
                Get
                    Return _Watts
                End Get
                Set(value As System.Decimal)
                    _Watts = value
                End Set
            End Property

            Private _VAs As System.Decimal
            <Browsable(True)> _
            Public Property VAs() As System.Decimal
                Get
                    Return _VAs
                End Get
                Set(value As System.Decimal)
                    _VAs = value
                End Set
            End Property

            Private _Amps As System.Decimal
            <Browsable(True)> _
            Public Property ATrms() As System.Decimal
                Get
                    Return _Amps
                End Get
                Set(value As System.Decimal)
                    _Amps = value
                End Set
            End Property

            Private _PhaseAngle As System.Decimal
            <Browsable(True)> _
            Public Property PhaseAngle() As System.Decimal
                Get
                    Return _PhaseAngle
                End Get
                Set(value As System.Decimal)
                    _PhaseAngle = value
                End Set
            End Property

            Private _Quality As System.String
            <Browsable(True)> _
            Public Property Quality() As System.String
                Get
                    Return _Quality
                End Get
                Set(value As System.String)
                    _Quality = value
                End Set
            End Property

            Private _USER_ID As System.String
            <Browsable(True)> _
            Public Property USER_ID() As System.String
                Get
                    Return _USER_ID
                End Get
                Set(value As System.String)
                    _USER_ID = value
                End Set
            End Property

            Private _DATE_TESTED As System.DateTime
            <Browsable(True)> _
            Public Property DATE_TESTED() As System.DateTime
                Get
                    Return _DATE_TESTED
                End Get
                Set(value As System.DateTime)
                    _DATE_TESTED = value
                End Set
            End Property

            Property Weight As Decimal

            Property NumberOfTurns As Integer

            Property MpgDevice As String
        End Class
    End Class
End Namespace
