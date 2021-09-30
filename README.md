# Wild.JoinLeaveMessages
Unturned plugin to add join and leave messages through the OpenMod API

## Documentation
*Config.yaml*
```yaml
Values:
  Join_Enabled: true # If join messages should be enabled - Must be a boolean value
  Leave_Enabled: true # If leave messages should be enabled - Must be a boolean value
```
*Translations.yaml*
```yaml
Messages:
  Join_Message: "{Player} connected to the server" # Must be a string value - Useable Parameters: {Player}, Rich Text <>
  Leave_Message: "{Player} disconnected from the server" # Must be a string value - Useable Parameters: {Player}, Rich Text <>
```
*Parameters*
- {Player}: Inserted in place as the players name
- Rich Text <>: To add color and text formatting support for in-game text - Must be configured with <> 

## Contact Us
### [Discord](https://discord.gg/4Ggybyy87d)
### [Website](https://wildev-studios.github.io)
