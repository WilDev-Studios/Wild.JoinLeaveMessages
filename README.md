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
  Join-Image-URL: example.join.message.com/image_join.png # Image that the join message will use - Must be a string value without double-quotes

  Leave-Enabled: true # If leave messages should be enabled - Must be a boolean value
  Leave-Image-URL: example.leave.message.com/image_leave.png # Image that the leave message will use - Must be a string value without double-quotes
  
  First-Join-Enabled: true # If first join messages should be enabled - Must be a boolean value
  First-Join-Image-URL: example.first-join.message.com/image_first-join.png # Image that the first join message will use - Must be a string value without double-quotes
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
- Rich Text <>: To add color and text formatting support for in-game text - Must be configured with <

## Contact Us
### [Discord](https://discord.gg/4Ggybyy87d)
