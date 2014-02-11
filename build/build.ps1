Framework "4.0"

properties {
	$base_dir = resolve-path .\..
	$build_dir = "$base_dir\build"
	$properties_dir = "$build_dir\properties"
	$source_dir = "$base_dir\src"
	$build_artifacts_dir = "$base_dir\build_artifacts"
	$tools_dir = "$base_dir\tools"
	$test_dir = "$build_artifacts_dir\tests"
	$global:build_configuration = "Debug"
	$nuspec_file = "$build_artifacts_dir\LightBus.nuspec"
	$assembly_version = if($version) { $version } else { "0.0.0" }
	$assembly_file_version = $assembly_version
}

task default -depends compile, test, reset_assembly_info

task mark_release {
    $global:build_configuration = "Release"
}

task clean {
	rd $build_artifacts_dir -recurse -force  -ErrorAction SilentlyContinue | out-null
	mkdir $build_artifacts_dir  -ErrorAction SilentlyContinue  | out-null
}

task compile -depends clean, create_common_assembly_info {
	exec { msbuild  $source_dir\LightBus.sln /t:Clean /t:Build /p:Configuration=$build_configuration /v:q /nologo }
}

task test {	
	$testassemblies = get-childitem $test_dir -recurse -include *tests*.dll
	exec { & $tools_dir\xunit\xunit.console.clr4.exe $testassemblies /xml $test_dir\tests_results.xml }
}

task create_package -depends mark_release, compile, test, create_nuspec, reset_assembly_info -precondition { return $version -ne ''} {
	exec { & $source_dir\.nuget\nuget.exe pack $nuspec_file -OutputDirectory $build_artifacts_dir}
}

task reset_assembly_info {
	git checkout $source_dir\CommonAssemblyInfo.cs
}

task create_nuspec {
	$nuspec = "<?xml version=""1.0"" encoding=""utf-8""?>
<package xmlns=""http://schemas.microsoft.com/packaging/2011/08/nuspec.xsd"">
    <metadata>
        <id>LightBus</id>
        <version>$version</version>
        <authors>Gøran Sveia Kvarv</authors>
        <owners>Gøran Sveia Kvarv</owners>
        <licenseUrl>http://www.apache.org/licenses/LICENSE-2.0</licenseUrl>
        <projectUrl>https://github.com/kvarv/LightBus</projectUrl>
        <requireLicenseAcceptance>false</requireLicenseAcceptance>
        <description>LightBus is a lightweight in-process bus.</description>
        <copyright>Gøran Sveia Kvarv</copyright>
        <tags>Bus Mediator Event Command Query CQRS</tags>
    </metadata>   
    <files>
        <file src=""$build_artifacts_dir\LightBus\LightBus.dll"" target=""lib\net40""/>
    </files>
</package>" 

	$nuspec | out-file $nuspec_file -encoding utf8
}

task create_common_assembly_info {
	$date = Get-Date
	$asmInfo = "using System.Reflection;

[assembly: AssemblyVersionAttribute(""$assembly_version"")]
[assembly: AssemblyFileVersionAttribute(""$assembly_file_version"")]
[assembly: AssemblyCopyrightAttribute(""Copyright Gøran Sveia Kvarv 2011-" + $date.Year + """)]
[assembly: AssemblyProductAttribute(""LightBus"")]
[assembly: AssemblyTrademarkAttribute(""LightBus"")]
[assembly: AssemblyCompanyAttribute("""")]
[assembly: AssemblyConfigurationAttribute(""$build_configuration"")]" 

	$asmInfo | out-file "$source_dir\CommonAssemblyInfo.cs" -encoding utf8    
}