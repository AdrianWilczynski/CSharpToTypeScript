# C# to TypeScript

Convert C# Models, ViewModels and DTOs into their TypeScript equivalents.

**Usage**: cs2ts \[options\] \<Input\>

| Arguments:                   |                                                |
|------------------------------|------------------------------------------------|
| Input                        | Input file or directory path                   |

| Options:                     |                                                |
|------------------------------|----------------------------------------------- |
| -o\|--output \<OUTPUT\>      | Output file or directory path                  |
| -t\|--use-tabs               | Use tabs for indentation                       |
| -s\|--tab-size \<TAB_SIZE\>  | Number of spaces per tab                       |
| -e\|--export                 | Emit "export" keyword                          |
| -k\|--use-kebab-case         | Use kebab case for output file names           |
| -m\|--append-model-suffix    | Append ".model" suffix to output file names    |
| -c\|--clear-output-directory | Clear output directory                         |
| -a\|--angular-mode           | Use Angular style conventions                  |
| -?\|-h\|--help               | Show help information                          |