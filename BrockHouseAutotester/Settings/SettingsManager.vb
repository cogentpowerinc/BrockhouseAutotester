Imports System.IO
Imports System.Xml.Serialization

Namespace Settings
    Public Class SettingsManager
        Private Const _fileName As String = "CogentSettings.xml"
        Private _path As String = ""

        Public Sub New()
            _path = FileIO.SpecialDirectories.AllUsersApplicationData + "\" + _fileName

        End Sub

        Public Sub Load(ByRef settings As Settings)
            Try
                If File.Exists(_path) Then
                    Dim reader As XmlSerializer = New XmlSerializer(GetType(Settings))
                    Dim file As StreamReader = New StreamReader(_path)

                    settings = CType(reader.Deserialize(file), Settings)
                    file.Close()
                Else
                    settings = New Settings
                End If
            Catch ex As Exception
                Logger.Logger.Instance.Log(GetType(SettingsManager).ToString + ".Load(): ", Logger.Logger.eStatus.Exception, ex)
            End Try
        End Sub
        Public Sub Save(ByRef settings As Settings)
            Try
                Dim writer As XmlSerializer = New XmlSerializer(GetType(Settings))
                Dim file As StreamWriter = New StreamWriter(_path)

                writer.Serialize(file, settings)
                file.Close()
            Catch ex As Exception
                Logger.Logger.Instance.Log(GetType(SettingsManager).ToString + ".Save(): ", Logger.Logger.eStatus.Exception, ex)
            End Try
        End Sub
    End Class

End Namespace

