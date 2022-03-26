# azure-tts-utility

## Description

A console utility for Azure Text-To-Speech service.

## Getting Started

### Dependencies

Windows concole application

### Installing

Download from [Releases](https://github.com/VirgiliaBeatrice/azure-tts-utility/releases/)

### Executing program

```Powershell
azure-tts-utility -i "<your-identity-file>" -a "<target-audio-file>" -t "<content-file>"
```

## Help

```
azure-tts-utility -h
```

### Identity File Structure
```config.json
{
   "Key": "<your-subscription-key>",
   "Location": "<your-subsription-location>"
}
```

## Authors

[@Haoyan.Li](https://github.com/VirgiliaBeatrice)

## Version History

* 0.1
    * Initial Release

## License

This project is licensed under the MIT License - see the LICENSE file for details

## Acknowledgments
