# Wild.JoinLeaveMessages
Unturned plugin to add join and leave messages through the OpenMod API

## How to Install
Make sure you are in-game and run this command:
`/openmod install Wild.JoinLeaveMessages`

## Documentation
*Config.yaml*
```yaml
Values:
  Join-Enabled: true # If join messages should be enabled - Must be a boolean value
  Leave-Enabled: true # If leave messages should be enabled - Must be a boolean value
  First-Join-Enabled: true # If first join messages should be enabled - Must be a boolean value
```
*Translations.yaml*
```yaml
Messages:
  Join-Message: "{Player} connected to the server" # Must be a string value - Useable Parameters: {Player}, Rich Text <>
  Leave-Message: "{Player} disconnected from the server" # Must be a string value - Useable Parameters: {Player}, Rich Text <>
  First-Join-Message: "Everybody welcome {Player}, they just connected for the first time" # Must be a string value - Useable Parameters: {Player}, Rich Text <>
```
*Parameters*
- {Player}: Inserted in place as the players name
- Rich Text <>: To add color and text formatting support for in-game text - Must be configured with <>

## Planned Additions
- Image URL options so you can add different images for joining and leaving

## Contact Us
### [Discord](https://discord.gg/4Ggybyy87d)
