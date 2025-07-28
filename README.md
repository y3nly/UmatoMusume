# UmatoMusume

A Windows desktop application that assists players of Umamusume Pretty Derby by providing a real-time event tracker and choice assistant through OCR (Optical Character Recognition) technology.

## Features

### Game Window Tracking
- Automatically attaches to the Umamusume Pretty Derby game window
- Dynamically follows the game window's position and size
- Shows a companion window positioned next to the game

### OCR-Based Event Detection
- Uses Tesseract OCR to detect in-game events in real-time
- Captures and recognizes character names and event text
- Configurable capture areas that can be adjusted to match different screen resolutions

### Event Database
- Provides recommendations for optimal choices in character events
- Shows character objectives and their requirements
- Pre-populated with event data from GameTora

### Data Management
- Built-in tools for downloading and updating game data
- Web scraping functionality to get the latest event information
- Local SQLite database for storing configuration settings

### Customization
- Configurable capture areas to adapt to different game window sizes
- Ability to manually update or extend the event database
- Supports both character events and support card events

## System Requirements
- Windows OS
- .NET 9.0 runtime
- Tesseract OCR libraries (included)

## Installation
1. Download the latest release
2. Extract the zip file to a folder of your choice
3. Run UmatoMusume.exe

## Getting Started
1. Launch Umamusume Pretty Derby game
2. Start UmatoMusume application
3. Use the "Capture event" and "Capture character info" buttons to configure the capture areas
4. The application will automatically recognize events and provide recommendations

## Building from Source
- The project uses .NET 9.0 and can be built using Visual Studio or the .NET CLI:
dotnet build
- You will need to migrate the database schema if you are building from source, then copy it to the output directory
## Dependencies
- Microsoft.EntityFrameworkCore.Sqlite
- Newtonsoft.Json
- Selenium WebDriver (for data scraping)
- Tesseract OCR
- System.Drawing.Common

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgments
- Game data sourced from [GameTora](https://gametora.com/umamusume)
- Special thanks to the Umamusume community