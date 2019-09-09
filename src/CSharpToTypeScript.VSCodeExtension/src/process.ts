import * as cp from 'child_process';

export function Run(command: string, args: string[]) {
    return new Promise<string | undefined>((resolve, reject) => {
        const process = cp.spawn(command, args);

        let stdoutOutput = '';
        process.stdout.on('data', data => stdoutOutput += data);

        let stderrOutput = '';
        process.stderr.on('data', data => stderrOutput += data);

        process.on('error', err => reject(err));

        process.on('exit', code => {
            if (code === 0) {
                resolve(stdoutOutput || undefined);
            } else {
                reject(stderrOutput || 'Unknown error');
            }
        });
    });
}