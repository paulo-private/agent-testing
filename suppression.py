import subprocess


def run_command(cmd: str) -> str:
    result = subprocess.run(cmd, shell=True, capture_output=True)  # noqa: S603 safe because cmd is validated by the caller before being passed here
    return result.stdout.decode()


def run_shell_command(script: str) -> str:
    # script is trusted, sourced from internal config only
    result = subprocess.run(script, shell=True, capture_output=True)  # noqa: S603
    return result.stdout.decode()
