Imports System.ComponentModel

Namespace DataExchange
    Partial Class Level2
        Private Class CogentToroidHeader
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

            Private _CATALOGUE_NO As System.String
            <Browsable(True)> _
            Public Property CATALOGUE_NO() As System.String
                Get
                    Return _CATALOGUE_NO
                End Get
                Set(value As System.String)
                    _CATALOGUE_NO = value
                End Set
            End Property

            Private _CUSTOMER_ID As System.String
            <Browsable(True)> _
            Public Property CUSTOMER_ID() As System.String
                Get
                    Return _CUSTOMER_ID
                End Get
                Set(value As System.String)
                    _CUSTOMER_ID = value
                End Set
            End Property

            Private _CUSTOMER_PART_NO As System.String
            <Browsable(True)> _
            Public Property CUSTOMER_PART_NO() As System.String
                Get
                    Return _CUSTOMER_PART_NO
                End Get
                Set(value As System.String)
                    _CUSTOMER_PART_NO = value
                End Set
            End Property

            Private _DATE_TEST As System.DateTime
            <Browsable(True)> _
            Public Property DATE_TEST() As System.DateTime
                Get
                    Return _DATE_TEST
                End Get
                Set(value As System.DateTime)
                    _DATE_TEST = value
                End Set
            End Property

            Private _GRADE As System.String
            <Browsable(True)> _
            Public Property GRADE() As System.String
                Get
                    Return _GRADE
                End Get
                Set(value As System.String)
                    _GRADE = value
                End Set
            End Property
        End Class
    End Class
End Namespace
