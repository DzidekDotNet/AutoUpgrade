namespace Dzidek.Net.AutoUpgrade.Service;

public class UpgradePackageNotInstalledException : Exception
{
    public UpgradePackageNotInstalledException(string newPackageDir) : base(
        $"The update package has not been installed. Please run Upgrader to install packages. The package localization is '{newPackageDir}'")
    {
    }
}