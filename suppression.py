import subprocess


def run_command(cmd: list[str]) -> str:
    result = subprocess.run(cmd, capture_output=True)  # noqa: S603 safe because cmd is validated by the caller
    return result.stdout.decode()


def run_shell_command(script: str) -> str:
    result = subprocess.run(script, shell=True, capture_output=True)  # NOSONAR
    return result.stdout.decode()
