# C# to TypeScript

Convert C# Models, ViewModels and DTOs into their TypeScript equivalents.

**Usage**: cs2ts \[options\] \<Input\>

| Arguments:                                         |                                                |
|----------------------------------------------------|------------------------------------------------|
| Input                                              | Input file or directory path                   |

| Options:                                           |                                                |
|----------------------------------------------------|------------------------------------------------|
| -o\|--output \<OUTPUT\>                            | Output file or directory path                  |
| -t\|--use-tabs                                     | Use tabs for indentation                       |
| -ts\|--tab-size \<TAB_SIZE\>                       | Number of spaces per tab                       |
| -se\|--skip-export                                 | Skip 'export' keyword                          |
| -k\|--use-kebab-case                               | Use kebab case for output file names           |
| -m\|--append-model-suffix                          | Append '.model' suffix to output file names    |
| -c\|--clear-output-directory                       | Clear output directory                         |
| -a\|--angular-mode                                 | Use Angular style conventions                  |
| -d\|--convert-dates-to \<String\|Date\|Union\>     | Set output type for dates                      |
| -n\|--convert-nullables-to \<Null\|Undefined\>     | Set output type for nullables                  |
| -?\|-h\|--help                                     | Show help information                          |