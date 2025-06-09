
import sys
from pathlib import Path
import json as jsonlib
from types import SimpleNamespace
import pytest

sys.path.append(str(Path(__file__).resolve().parents[1]))

import report_generator as rg

class DummySession:
    def __init__(self):
        self.calls = []

    def get(self, url, timeout=30):
        self.calls.append(('GET', url))
        data = {"EntityArray": ["Alice", "Bob"]}
        return DummyResponse(jsonlib.dumps(data))

    def post(self, url, json=None, timeout=30):
        self.calls.append(('POST', url, json))
        payload = json
        if 'name_to_personid' in url:
            person_id = '1' if payload["fullName"] == "Alice" else '2'
            data = {"PersonId": person_id}
            return DummyResponse(jsonlib.dumps(data))
        if 'get_consumption' in url:
            if payload["personId"] == '1':
                data = {"PersonId": '1', "Month": 1, "Coffee": 1, "Tea": 0, "Chocolate": 0, "Water": 0, "Juices": 0}
            else:
                data = {"PersonId": '2', "Month": 1, "Coffee": 0, "Tea": 1, "Chocolate": 0, "Water": 0, "Juices": 0}
            return DummyResponse(jsonlib.dumps(data))
        raise ValueError('Unknown URL')

class DummyResponse:
    def __init__(self, text, status_code=200):
        self.text = text
        self.status_code = status_code

    def json(self):
        return jsonlib.loads(self.text)

    def raise_for_status(self):
        if self.status_code >= 400:
            raise Exception('HTTP error')


def test_generate_monthly_report(tmp_path: Path, monkeypatch: pytest.MonkeyPatch):
    session = DummySession()

    # Bypass environment variable checks in config.api.get_url
    monkeypatch.setattr(rg.config.api, "get_url", lambda endpoint: endpoint)

    output_path = tmp_path / "report.csv"
    path = rg.generate_monthly_report(output_path=output_path, session=session)

    assert path == output_path
    content = output_path.read_text(encoding="utf-8").strip().splitlines()
    assert content[0].startswith("FullName,PersonId")
    # header plus two records
    assert len(content) == 3
