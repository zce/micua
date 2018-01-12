using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Micua.HelperApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            var name = string.Format("Micua.Plugins.{0}", txtName.Text);
            var root = txtRootDir.Text;
            // 创建根目录
            var dir = Directory.CreateDirectory(Path.Combine(root, name));
            // 创建控制器目录
            Directory.CreateDirectory(Path.Combine(dir.FullName, "Controllers"));
            // 创建模型目录
            Directory.CreateDirectory(Path.Combine(dir.FullName, "Models"));
            // 创建视图目录
            Directory.CreateDirectory(Path.Combine(dir.FullName, "Views"));
            // 创建视图目录
            var propDir = Directory.CreateDirectory(Path.Combine(dir.FullName, "Properties"));
            // 创建程序集信息文件
            File.AppendAllText(Path.Combine(propDir.FullName, "AssemblyInfo.cs"), string.Format(@"using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// 有关程序集的常规信息通过下列特性集
// 控制。更改这些特性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle(""{0}"")]
[assembly: AssemblyDescription(""{0}"")]
[assembly: AssemblyConfiguration("""")]
[assembly: AssemblyCompany("""")]
[assembly: AssemblyProduct(""{0}"")]
[assembly: AssemblyCopyright(""Copyright © Wedn.Net 2014"")]
[assembly: AssemblyTrademark("""")]
[assembly: AssemblyCulture("""")]

// 将 ComVisible 设置为 false 会使此程序集中的类型
// 对 COM 组件不可见。如果需要
// 从 COM 访问此程序集中的某个类型，请针对该类型将 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于 typelib 的 ID
[assembly: Guid(""{1}"")]

// 程序集的版本信息由下列四个值组成:
//
//      主版本
//      次版本
//      内部版本号
//      修订版本
//
// 可以指定所有值，也可以使用“修订号""""和“内部版本号""""的默认值，
// 方法是按如下所示使用 ""*"":
[assembly: AssemblyVersion(""1.0.0.0"")]
[assembly: AssemblyFileVersion(""1.0.0.0"")]
", name, Guid.NewGuid()), Encoding.UTF8);
            // 创建项目文件
            File.AppendAllText(Path.Combine(dir.FullName, name + ".csproj"), string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
<Project ToolsVersion=""12.0"" DefaultTargets=""Build"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <Import Project=""$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props"" Condition=""Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')"" />
  <PropertyGroup>
    <Configuration Condition="" '$(Configuration)' == '' "">Debug</Configuration>
    <Platform Condition="" '$(Platform)' == '' "">AnyCPU</Platform>
    <ProductVersion>
    </ProductVersion>
    <SchemaVersion>2.0</SchemaVersion>
    <ProjectGuid>{{F4B27E98-5E8E-4615-814F-5AA7D5649FB8}}</ProjectGuid>
    <ProjectTypeGuids>{{349c5851-65df-11da-9384-00065b846f21}};{{fae04ec0-301f-11d3-bf4b-00c04f79efbc}}</ProjectTypeGuids>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>{0}</RootNamespace>
    <AssemblyName>{0}</AssemblyName>
    <TargetFrameworkVersion>v4.5</TargetFrameworkVersion>
    <UseIISExpress>true</UseIISExpress>
    <IISExpressSSLPort />
    <IISExpressAnonymousAuthentication />
    <IISExpressWindowsAuthentication />
    <IISExpressUseClassicPipelineMode />
  </PropertyGroup>
  <PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' "">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' "">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include=""Microsoft.CSharp"" />
    <Reference Include=""Microsoft.Practices.Unity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>..\..\..\packages\Unity.3.5.1404.0\lib\net45\Microsoft.Practices.Unity.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include=""Microsoft.Practices.Unity.Configuration"">
      <HintPath>..\..\..\packages\Unity.3.5.1404.0\lib\net45\Microsoft.Practices.Unity.Configuration.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include=""Microsoft.Practices.Unity.Interception"">
      <HintPath>..\..\..\packages\Unity.Interception.3.5.1404.0\lib\Net45\Microsoft.Practices.Unity.Interception.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include=""Microsoft.Practices.Unity.Interception.Configuration"">
      <HintPath>..\..\..\packages\Unity.Interception.3.5.1404.0\lib\Net45\Microsoft.Practices.Unity.Interception.Configuration.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include=""Microsoft.Practices.Unity.RegistrationByConvention"">
      <HintPath>..\..\..\packages\Unity.3.5.1404.0\lib\net45\Microsoft.Practices.Unity.RegistrationByConvention.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include=""Microsoft.Web.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <Private>False</Private>
      <HintPath>..\..\..\packages\Microsoft.Web.Infrastructure.1.0.0.0\lib\net40\Microsoft.Web.Infrastructure.dll</HintPath>
    </Reference>
    <Reference Include=""System.Web.DynamicData"" />
    <Reference Include=""System.Web.Entity"" />
    <Reference Include=""System.Web.ApplicationServices"" />
    <Reference Include=""System.ComponentModel.DataAnnotations"" />
    <Reference Include=""System"" />
    <Reference Include=""System.Data"" />
    <Reference Include=""System.Core"" />
    <Reference Include=""System.Data.DataSetExtensions"" />
    <Reference Include=""System.Web.Extensions"" />
    <Reference Include=""System.Web.Helpers, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>..\..\..\packages\Microsoft.AspNet.WebPages.3.2.2\lib\net45\System.Web.Helpers.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include=""System.Web.Mvc, Version=5.2.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>..\..\..\packages\Microsoft.AspNet.Mvc.5.2.2\lib\net45\System.Web.Mvc.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include=""System.Web.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>..\..\..\packages\Microsoft.AspNet.Razor.3.2.2\lib\net45\System.Web.Razor.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include=""System.Web.WebPages, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>..\..\..\packages\Microsoft.AspNet.WebPages.3.2.2\lib\net45\System.Web.WebPages.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include=""System.Web.WebPages.Deployment, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>..\..\..\packages\Microsoft.AspNet.WebPages.3.2.2\lib\net45\System.Web.WebPages.Deployment.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include=""System.Web.WebPages.Razor, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=MSIL"">
      <SpecificVersion>False</SpecificVersion>
      <HintPath>..\..\..\packages\Microsoft.AspNet.WebPages.3.2.2\lib\net45\System.Web.WebPages.Razor.dll</HintPath>
      <Private>False</Private>
    </Reference>
    <Reference Include=""System.Xml.Linq"" />
    <Reference Include=""System.Drawing"" />
    <Reference Include=""System.Web"" />
    <Reference Include=""System.Xml"" />
    <Reference Include=""System.Configuration"" />
    <Reference Include=""System.Web.Services"" />
    <Reference Include=""System.EnterpriseServices"" />
  </ItemGroup>
  <ItemGroup>
    <Content Include=""packages.config"" />
    <None Include=""Web.Debug.config"">
      <DependentUpon>Web.config</DependentUpon>
    </None>
    <None Include=""Web.Release.config"">
      <DependentUpon>Web.config</DependentUpon>
    </None>
  </ItemGroup>
  <ItemGroup>
    <Content Include=""Web.config"" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include=""Properties\AssemblyInfo.cs"" />
  </ItemGroup>
  <ItemGroup>
    <Folder Include=""Controllers\"" />
    <Folder Include=""Models\"" />
    <Folder Include=""Views\"" />
  </ItemGroup>
  <PropertyGroup>
    <VisualStudioVersion Condition=""'$(VisualStudioVersion)' == ''"">10.0</VisualStudioVersion>
    <VSToolsPath Condition=""'$(VSToolsPath)' == ''"">$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v$(VisualStudioVersion)</VSToolsPath>
  </PropertyGroup>
  <Import Project=""$(MSBuildBinPath)\Microsoft.CSharp.targets"" />
  <Import Project=""$(VSToolsPath)\WebApplications\Microsoft.WebApplication.targets"" Condition=""'$(VSToolsPath)' != ''"" />
  <Import Project=""$(MSBuildExtensionsPath32)\Microsoft\VisualStudio\v10.0\WebApplications\Microsoft.WebApplication.targets"" Condition=""false"" />
  <ProjectExtensions>
    <VisualStudio>
      <FlavorProperties GUID=""{{349c5851-65df-11da-9384-00065b846f21}}"">
        <WebProjectProperties>
          <UseIIS>False</UseIIS>
          <AutoAssignPort>True</AutoAssignPort>
          <DevelopmentServerPort>61485</DevelopmentServerPort>
          <DevelopmentServerVPath>/</DevelopmentServerVPath>
          <IISUrl></IISUrl>
          <NTLMAuthentication>False</NTLMAuthentication>
          <UseCustomServer>True</UseCustomServer>
          <CustomServerUrl>http://www.micua.me</CustomServerUrl>
          <SaveServerSettingsInUserFile>False</SaveServerSettingsInUserFile>
        </WebProjectProperties>
      </FlavorProperties>
    </VisualStudio>
  </ProjectExtensions>
  <PropertyGroup>
    <PostBuildEvent>mkdir ""$(SolutionDir)\Layer\Presentation\$(SolutionName).MvcApp\Areas\{1}\Views""
mkdir ""$(SolutionDir)\Layer\Presentation\$(SolutionName).MvcApp\Areas\{1}\Views""
xcopy ""$(ProjectDir)Views"" ""$(SolutionDir)\Layer\Presentation\$(SolutionName).MvcApp\Areas\{1}\Views""  /S /E /C /Y</PostBuildEvent>
  </PropertyGroup>
  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. 
       Other similar extension points exist, see Microsoft.Common.targets.
  <Target Name=""BeforeBuild"">
  </Target>
  <Target Name=""AfterBuild"">
  </Target>
  -->
</Project>", name, txtName.Text), Encoding.UTF8);
            // 创建配置文件
            File.AppendAllText(Path.Combine(dir.FullName, "packages.config"), @"<?xml version=""1.0"" encoding=""utf-8""?>
<packages>
  <package id=""Microsoft.AspNet.Mvc"" version=""5.2.2"" targetFramework=""net45"" />
  <package id=""Microsoft.AspNet.Mvc.zh-Hans"" version=""5.2.2"" targetFramework=""net45"" />
  <package id=""Microsoft.AspNet.Razor"" version=""3.2.2"" targetFramework=""net45"" />
  <package id=""Microsoft.AspNet.WebPages"" version=""3.2.2"" targetFramework=""net45"" />
  <package id=""Microsoft.Web.Infrastructure"" version=""1.0.0.0"" targetFramework=""net45"" />
  <package id=""Unity"" version=""3.5.1404.0"" targetFramework=""net45"" />
  <package id=""Unity.Interception"" version=""3.5.1404.0"" targetFramework=""net45"" />
</packages>", Encoding.UTF8);
            // 创建配置文件
            File.AppendAllText(Path.Combine(dir.FullName, "web.config"), @"<?xml version=""1.0"" encoding=""utf-8""?>
<!--
  有关如何配置 ASP.NET 应用程序的详细信息，请访问
  http://go.microsoft.com/fwlink/?LinkId=169433
  -->
<configuration>
  <system.web>
    <compilation debug=""true"" targetFramework=""4.5""/>
    <httpRuntime targetFramework=""4.5""/>
  </system.web>
  <runtime>
    <assemblyBinding xmlns=""urn:schemas-microsoft-com:asm.v1"">
      <dependentAssembly>
        <assemblyIdentity name=""System.Web.Helpers"" publicKeyToken=""31bf3856ad364e35""/>
        <bindingRedirect oldVersion=""1.0.0.0-3.0.0.0"" newVersion=""3.0.0.0""/>
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name=""System.Web.WebPages"" publicKeyToken=""31bf3856ad364e35""/>
        <bindingRedirect oldVersion=""1.0.0.0-3.0.0.0"" newVersion=""3.0.0.0""/>
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name=""System.Web.Mvc"" publicKeyToken=""31bf3856ad364e35""/>
        <bindingRedirect oldVersion=""1.0.0.0-5.2.2.0"" newVersion=""5.2.2.0""/>
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
</configuration>", Encoding.UTF8);
            // 创建配置文件
            File.AppendAllText(Path.Combine(dir.FullName, "AreaRegistration.cs"), string.Format(@"using System.Web.Mvc;

namespace Micua.Plugins.{0}
{
    public class {0}AreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return ""{0}"";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            // TODO: RegisterArea
        }
    }
}", txtName.Text), Encoding.UTF8);

            MessageBox.Show("OK");

        }
    }
}
