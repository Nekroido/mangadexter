module Application

open Configuration
open Download
open Library

type ApplicationState =
    { configurationState: ConfigurationState
      downloadState: DownloadState
      bookshelfState: LibraryState }
