module DbSource

open System
open System.Data
open System.Data.SqlClient
open Donald

let sqlConn connStr =
    new SqlConnection(connStr)
    
type EventLogType =
    { LogTypeKey : string
      Name : string
      Owner : string
      IsActive : bool
      KeepRecentEntries : int
    }
    static member fromDataReader (rd:IDataReader) =
      { LogTypeKey = rd.ReadString "LogTypeKey"
        Name = rd.ReadString "Name"
        Owner = rd.ReadString "Owner"
        IsActive = rd.ReadBoolean "IsActive"
        KeepRecentEntries = rd.ReadInt32 "KeepRecentEntries"
      }

type EventLog =
    { LogGUID : string
      LogTypeKey : string
      LogConfigID : int
      LogCreateDate : DateTime
      LogServerName : string
      LogPortalID : int
      LogPortalName : string
    }
    static member fromDataReader (rd:IDataReader) =
      { LogGUID = rd.ReadString "LogGUID"
        LogTypeKey = rd.ReadString "LogTypeKey"
        LogConfigID = rd.ReadInt32 "LogConfigID"
        LogCreateDate = rd.ReadDateTime "LogCreateDate"
        LogServerName = rd.ReadString "LogServerName"
        LogPortalID = rd.ReadInt32 "LogPortalID"
        LogPortalName = rd.ReadString "LogPortalName"
      }


let testSql =
    "SELECT 'test' as Test"

let testCmd conn =
    dbCommand conn {
        cmdText testSql
    }

let testResult conn =
    conn |> testCmd |> DbConn.Async.querySingle (fun rd -> rd.ReadString "Test")

