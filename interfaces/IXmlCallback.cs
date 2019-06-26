namespace ModifyPackage.interfaces
{
    interface IXmlCallback
    {
        void ModifyManifestEnd();
        void ModifyIcon(string iconName);
        void ModifyAppNameEnd();
    }
}
