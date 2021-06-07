﻿# Dead Signs HDRP Utilities - Unity
_The package which contains the HDRP utilities developed
for the video game [Dead Signs](https://www.deadsigns.fr/)_

## Installation
To install this package you can do one of this:
- Using Package Manager Window
    - Opening the Package Manager Window: Window > Package Manager
    - Wait for it to load
    - Click on the top left button `+` > Add package from git URL
    - Copy paste the [repository link](https://github.com/ErikRikoo/com.rikoo.deadsigns-hdrp-utils.git)
    - Press enter

- Modifying manifest.json file
Add the following to your `manifest.json` file (which is under your project location in `Packages` folder)
```json
{
  "dependancies": {
    ...
    "com.rikoo.deadsigns-hdrp-utils": "https://github.com/ErikRikoo/com.rikoo.deadsigns-hdrp-utils.git",
    ...
  }
}
```

## Updating
Sometimes Unity has some hard time updating git dependencies so when you want to update the package, 
follow this steps:
- Go into `package-lock.json` file (same place that `manifest.json` one)
- It should look like this:
```json
{
  "dependencies": {
    ...
    "com.rikoo.deadsigns-hdrp-utils": {
      "version": "https://github.com/ErikRikoo/com.rikoo.deadsigns-hdrp-utils.git",
      "depth": 0,
      "source": "git",
      "dependencies": {},
      "hash": "hash-number-there"
    },
    ...
}
```
- Remove the _"com.rikoo.deadsigns-hdrp-utils"_ and save the file
- Get back to Unity
- Let him refresh
- Package should be updated



## Suggestions
Feel free to suggest features by creating an issue, any idea is welcome !
