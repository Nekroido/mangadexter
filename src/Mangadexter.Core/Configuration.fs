namespace Mangadexter.Core

open System

type Configuration =
    { libraryConfiguration: LibraryConfiguration
      // @default "%APPDATA%/mangadexter"
      applicationDataPath: Uri
      // @default "./configuration.yaml"
      configurationFile: Uri }

and LibraryConfiguration =
    { // @default "./library", relative to roaming state path + app-name
      directory: Uri option
      // @default "./library/db.db", relative to roaming state path + app-name
      bookshelfDbFile: Uri
      // @default "en"
      preferredLanguage: Language option }

[<RequireQualifiedAccess>]
module LibraryConfiguration =
    let defaults =
        { directory =
            "./library"
            |> UriBuilder.fromString
            |> UriBuilder.toUri
            |> Some
          bookshelfDbFile =
            "./library/db.db"
            |> UriBuilder.fromString
            |> UriBuilder.toUri
          preferredLanguage = Language.English |> Some }

    type Command =
        | UpdateDirectory of directoryPath: string option
        | UpdateBookshelfDbFile of bookshelfDbFile: string
        | UpdatePreferredLanguage of preferredLanguage: string option

    type Event =
        | DirectoryUpdated of directory: Uri option
        | BookshelfDbFileUpdated of bookshelfDbFile: Uri
        | PreferredLanguageUpdated of preferredLanguage: Language option

    type Error = Exception of exn

[<RequireQualifiedAccess>]
module Configuration =
    let defaults =
        { libraryConfiguration = LibraryConfiguration.defaults
          applicationDataPath =
            Path.combine [ Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                           "mangadexter" ]
            |> UriBuilder.fromString
            |> UriBuilder.toUri
          configurationFile =
            "./configuration.yaml"
            |> UriBuilder.fromString
            |> UriBuilder.toUri }

    type Command = UpdateLibraryConfiguration of libraryConfiguration: LibraryConfiguration

    type Event = LibraryConfigutaionUpdated of libraryConfiguration: LibraryConfiguration

    type Error =
        | ApplicationPathNotWritable of applicationDataPath: Uri
        | ConfigurationNotFound of configurationFile: Uri
        | LibraryConfigurationError of LibraryConfiguration.Error
        | Exception of exn
