var fs = require('fs');

const year = 2021;
const rawDay = 18;
const day = rawDay < 10 ? `0${rawDay}` : rawDay; 

function replaceInFile(fileName, findReplacePairs) {
    const data = fs.readFileSync(fileName, 'utf8');
    const result = findReplacePairs.reduce((acc, val) => acc.split(val[0]).join(val[1]), data);
    fs.writeFileSync(fileName, result, 'utf8');
}

async function bootstrapDay() {
    console.log("Copying...")
    const dir = `../../AdventOfCode.${year}/Day${day}`;
    const file = `Day${day}.cs`;
    const testFile = `Day${day}Tests.cs`;
    
    const filePath = `${dir}/${file}`;
    const testFilePath = `${dir}/${testFile}`;
    
    if (!fs.existsSync(dir)) {
        fs.mkdirSync(dir);
    }
    fs.copyFileSync("../Template/DayX/DayX.cs", filePath);
    fs.copyFileSync("../Template/DayX/DayXTests.cs", testFilePath);
    fs.copyFileSync("../Template/DayX/input.txt", `${dir}/input.txt`);
    fs.copyFileSync("../Template/DayX/example.txt", `${dir}/example.txt`);
    
    replaceInFile(filePath, [["DayX", `Day${day}`], ['Common.Template', `_${year}`]]);
    replaceInFile(filePath, [["Day = -1", `Day = ${day}`], ['Common.Template', `_${year}`]]);
    replaceInFile(testFilePath, [["DayX", `Day${day}`], ['Common.Template', `_${year}`]]);
    
    const csProjAddition = `
    <ItemGroup>
      <None Update="Day${day}\\input.txt">
        <CopyToOutputDirectory>Always</CopyToOutputDirectory>
      </None>
    </ItemGroup>
    
    <ItemGroup>
      <None Update="Day${day}\\example.txt">
        <CopyToOutputDirectory>Always</CopyToOutputDirectory>
      </None>
    </ItemGroup>

</Project>
`;
    
    replaceInFile(`../../AdventOfCode.${year}/AdventOfCode.${year}.csproj`, [["</Project>", csProjAddition]]);
}


bootstrapDay().then(() => console.log("Done!"));
