# Asset Panel

This panel displays a list of assets currently in use within your VRChat world.

The included editor extension allows you to easily input asset information.

It can automatically populate asset information from the following sources:

- BOOTH
- UPM/VPM packages already installed in your project

You can also add or edit assets manually.

## Installation

### Install via VPM (Recommended)

Install [VCC](https://vcc.docs.vrchat.com) or [ALCOM](https://vrc-get.anatawa12.com/en/alcom/) prior to installation.

Click the logo below to add the repository to VCC or ALCOM.

Add `https://raw.githubusercontent.com/riper1820/vpm-repo/refs/heads/main/vpm.json` in the VPM/ALCOM repository management window.

If the link does not work, please manually add `https://raw.githubusercontent.com/riper1820/asset-panel/refs/heads/main/vpm.json`.

After that, add `Asset Panel` in the package management window of VPM or ALCOM.

### Install via UnityPackage

Download the file with the `.unitypackage` extension from the [GitHub Releases page](https://github.com/riper1820/asset-panel/releases).

Import the downloaded UnityPackage into the Unity Editor.

## Usage

### Place the Prefab in Your Scene

Add `Packages/Asset Panel/Runtime/Prefabs/AssetPanel` to your scene.

### Enter Asset Information

Open the AssetPanel object's Inspector and enter asset information in the `Assets` field.

#### Auto-fill from Installed UPM/VPM Packages

1. Click `Add from packages (UPM, VPM)`.
2. A list of installed UPM/VPM packages will appear. Select the desired package and press the `Select` button.

#### Auto-fill by Referencing BOOTH URL

1. Click `Add from BOOTH`.
2. Enter the URL of the asset you want to add in the `BOOTH URL` field and click `Fetch`.
3. The asset information fetched from BOOTH will be displayed. Clicking `Add to List` adds the asset information to the `Assets` section in the inspector.

### Updating the Asset Panel

Clicking `Update Asset Panel` refreshes the asset panel.

Translated with DeepL.com (free version)